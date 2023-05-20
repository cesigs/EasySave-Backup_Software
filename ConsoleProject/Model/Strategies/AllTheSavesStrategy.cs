using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// Inheritance class  of the interface IStrategySaveType to use ExecuteSave()
    /// </summary>
    class AllTheSavesStrategy : IStrategySaveType
    {
        /// <summary>
        /// Will execute all the different save works registered on bdd.json
        /// will gather the informations to fill logs.json
        /// will gather the informations to update in real time the file state.json
        /// </summary>
        public void ExecuteSave()
        {
            string fileName = @"c:\bdd.json";
            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                var myPosts = JsonConvert.DeserializeObject<SaveWork[]>(justText);
                TimeSpan ts = new TimeSpan(0);
                string state = "Active";
                foreach (var post in myPosts)
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
                            foreach (string newPath in Directory.GetFiles(post.FileSource, "*.*", SearchOption.AllDirectories))
                            {
                                long actualFileSize = new System.IO.FileInfo(newPath).Length;
                                long sizeleft = dirSize - actualFileSize;
                                dirSize -= actualFileSize;
                                int filesLeft = totalFiles - i;

                                Stopwatch stopWatch = new Stopwatch();
                                stopWatch.Start();

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
                                        File.Copy(newPath, newPath.Replace(post.FileSource, post.destPath), true);
                                    }
                                }
                                else
                                {
                                    File.Copy(newPath, newPath.Replace(post.FileSource, post.destPath), true);
                                }
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                WriteLogs.WriteLogsOnJson(post.Name, newPath, post.destPath, ts);
                                WriteStates.WriteStatesOnJson(post.Name, newPath, post.destPath, totalFiles, totalSize, dirSize, filesLeft, state);
                                WriteLogs.WriteLogsOnXML(post.Name, newPath, post.destPath, ts);
                            }
                        }
                        catch
                        {
                            ts = new TimeSpan(-1);
                            string newPath = "error";
                            Console.WriteLine("Error cant find source of " + post.Name);
                            WriteLogs.WriteLogsOnJson(post.Name, newPath, post.destPath, ts);
                            WriteLogs.WriteLogsOnXML(post.Name, newPath, post.destPath, ts);
                        }
                    }
                    catch
                    {
                        ts = new TimeSpan(-1);
                        string newPath = "error";
                        Console.WriteLine("Error cant find source of " + post.Name);
                        WriteLogs.WriteLogsOnJson(post.Name, newPath, post.destPath, ts);
                        WriteLogs.WriteLogsOnXML(post.Name, newPath, post.destPath, ts);
                    }
                }
            }
        }
    }
}
