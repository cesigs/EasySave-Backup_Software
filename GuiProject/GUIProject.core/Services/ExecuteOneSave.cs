using Newtonsoft.Json;
using System.Diagnostics;

namespace GUIProject.core.Services.Strategies
{
    public class ExecuteOneSave
    {
        /// <summary>
        /// Gather the information to execute one the save work
        /// Launch the working function in a thread for multithreading
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="blockIfRunning"></param>
        /// <param name="threadlist"></param>
        /// <param name="extensionToCrypt"></param>
        /// <param name="manualResetEvent"></param>
        /// <param name="priorityFile"></param>
        public void ExecuteSave(string myId, string blockIfRunning, IList<Thread> threadlist, string extensionToCrypt, ManualResetEvent manualResetEvent, string priorityFile)
        {
            string fileName = @"c:\bdd.json";
            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                SaveWork[] myPosts = JsonConvert.DeserializeObject<SaveWork[]>(justText);
                TimeSpan ts = new TimeSpan(0);

                int myIdint = Int32.Parse(myId);

                string state = "Active";

                foreach (SaveWork post in myPosts)
                {
                    Thread t = new Thread(
                        ()=>
                        {
                            DoWork(blockIfRunning, post, state, ts, extensionToCrypt, myIdint, manualResetEvent, priorityFile);
                        }
                        );
                    t.Start();
                    threadlist.Add(t);
                }    
            }
        }

        /// <summary>
        /// Save every file and folder conatains in the source folder
        /// Decide according to the options given by the users to prioritize a particular type of file
        /// Decide according to the options given by the users to encrypt a particular type of file
        /// Decide according to the options given by the users to pause the execution while a blocking software is launched
        /// Check by using a manual reset event if the thread need to be paused
        /// Gather the informations to give and laucnh the writing of logs in JSON and XML but alsoo for the states file
        /// </summary>
        /// <param name="blockIfRunning"></param>
        /// <param name="post"></param>
        /// <param name="state"></param>
        /// <param name="ts"></param>
        /// <param name="extensionToCrypt"></param>
        /// <param name="manualResetEvent"></param>
        /// <param name="priorityFile"></param>
        public static void DoWork(string blockIfRunning, SaveWork post, string state, TimeSpan ts, string extensionToCrypt, int myIdint, ManualResetEvent manualResetEvent, string priorityFile)
        {
            if (post.id == myIdint)
            {
                try
                {
                    foreach (string dirPath in Directory.GetDirectories(post.FileSource, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(post.FileSource, post.destPath));
                        int fCount = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Length;
                    }
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(post.FileSource);
                        int i = 1;
                        int totalFiles = Directory.GetFiles(post.FileSource, "*.*", SearchOption.AllDirectories).Length;
                        long dirSize = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
                        long totalSize = dirSize;
                        string[] MyFiles = Directory.GetFiles(post.FileSource, "*.*", SearchOption.AllDirectories);
                        foreach (string file in MyFiles)
                        {
                            if (file.Contains(priorityFile))
                            {
                                MyFiles = MyFiles.Where(o => o != file).ToArray();
                                MyFiles = MyFiles.Prepend(file).ToArray();
                            }
                        }
                        foreach (string newPath in MyFiles)
                        {
                            manualResetEvent.WaitOne(Timeout.Infinite);
                            while ((Process.GetProcessesByName(blockIfRunning).Length > 0))
                            {
                                manualResetEvent.Reset();
                            }
                            manualResetEvent.Set();
                            Thread.Sleep(1000);
                            long actualFileSize = new System.IO.FileInfo(newPath).Length;
                            long sizeleft = dirSize - actualFileSize;
                            dirSize -= actualFileSize;
                            int filesLeft = totalFiles - i;

                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();

                            Stopwatch cryptTimeWatch = new Stopwatch();
                            TimeSpan cryptTime = new TimeSpan(0);

                            string myPath = Path.GetDirectoryName(newPath);
                            i += 1;
                            if (i < totalFiles + 1)
                            {
                                state = "Active";

                            }
                            else
                            {
                                state = "Ended";
                            }

                            if (post.type == "differential")
                            {
                                DateTime lastModifiedTime = File.GetLastWriteTime(newPath);
                                DateTime Test = Convert.ToDateTime(post.time);
                                int compareDateTime = DateTime.Compare(lastModifiedTime, Test);
                                if (compareDateTime > 0)
                                {
                                    if (newPath.Contains(extensionToCrypt))
                                    {
                                        cryptTimeWatch.Start();
                                        EncryptFile encrypt = new EncryptFile();
                                        encrypt.launchEncrypt(newPath, newPath.Replace(post.FileSource, post.destPath));
                                        cryptTimeWatch.Stop();
                                        cryptTime = cryptTimeWatch.Elapsed;
                                    }
                                    else
                                    {
                                        cryptTime = new TimeSpan(0);
                                    }
                                    File.Copy(newPath, newPath.Replace(post.FileSource, post.destPath), true);
                                }
                            }
                            else
                            {
                                if (newPath.Contains(extensionToCrypt))
                                {

                                    cryptTimeWatch.Start();
                                    EncryptFile encrypt = new EncryptFile();
                                    encrypt.launchEncrypt(newPath, newPath.Replace(post.FileSource, post.destPath));
                                    cryptTimeWatch.Stop();
                                    cryptTime = cryptTimeWatch.Elapsed;
                                }
                                else
                                {
                                    cryptTime = new TimeSpan(0);
                                }
                                File.Copy(newPath, newPath.Replace(post.FileSource, post.destPath), true);
                            }
                            stopWatch.Stop();
                            ts = stopWatch.Elapsed;
                            WriteLogs.WriteLogsOnJson(post.Name, newPath, post.destPath, ts, cryptTime);
                            WriteLogs.WriteLogsOnXML(post.Name, newPath, post.destPath, ts, cryptTime);
                            WriteStates.WriteStatesOnJson(post.Name, newPath, post.destPath, totalFiles, totalSize, dirSize, filesLeft, state);

                        }
                    }
                    catch
                    {
                        ts = new TimeSpan(-1);
                        TimeSpan cryptTime = new TimeSpan(-1);
                        string newPath = "error";
                        Console.WriteLine("Error cant find source of " + post.Name);
                        WriteLogs.WriteLogsOnJson(post.Name, newPath, post.destPath, ts, cryptTime);
                        WriteLogs.WriteLogsOnXML(post.Name, newPath, post.destPath, ts, cryptTime);
                    }
                }
                catch
                {
                    ts = new TimeSpan(-1);
                    TimeSpan cryptTime = new TimeSpan(-1);
                    string newPath = "error";
                    Console.WriteLine("Error cant find source of " + post.Name);
                    WriteLogs.WriteLogsOnJson(post.Name, newPath, post.destPath, ts, cryptTime);
                    WriteLogs.WriteLogsOnXML(post.Name, newPath, post.destPath, ts, cryptTime);
                }
            }
        }
    }
}
