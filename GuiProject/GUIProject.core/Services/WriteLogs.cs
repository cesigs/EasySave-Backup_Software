using Newtonsoft.Json;
using System.Text.Json;
using System.Xml;

namespace GUIProject
{
    public class WriteLogs
    {
        /// <summary>
        /// Allows to write the logs in JSON using the parameters given by executing on or all the saveworks
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="newPath"></param>
        /// <param name="destPath"></param>
        /// <param name="ts"></param>
        public static void WriteLogsOnJson(string Name, string newPath, string destPath, TimeSpan ts, TimeSpan cryptTime)
        {

            string elapsedTime = ts.ToString();
            string cryptTimeString = cryptTime.ToString();
            string fileName = @"c:\logs.json";

            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                List<Logs> myPosts = JsonConvert.DeserializeObject<List<Logs>>(justText);
                Logs logs = new Logs();
                logs.Name = Name;
                logs.FileSource = newPath;
                logs.destPath = $@"{destPath}\{Path.GetFileName(newPath)}";
                logs.transferTime = elapsedTime;
                long length;
                try
                {
                    length = new System.IO.FileInfo(newPath).Length;
                }
                catch
                {
                    length = 0;
                }

                logs.size = (int)length;
                logs.time = DateTime.Now.ToString();
                logs.cryptTime = cryptTimeString;
                myPosts.Add(logs);
                string json = System.Text.Json.JsonSerializer.Serialize(myPosts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
        }

        /// <summary>
        /// Allows to write the logs in XML using the parameters given by executing on or all the saveworks
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="newPath"></param>
        /// <param name="destPath"></param>
        /// <param name="ts"></param>
        /// <param name="cryptTime"></param>
        public static void WriteLogsOnXML(string Name, string newPath, string destPath, TimeSpan ts, TimeSpan cryptTime)
        {
            string elapsedTime = ts.ToString();
            string cryptTimeString = cryptTime.ToString();
            string fileName = @"c:\logs.xml";

            if (File.Exists(fileName))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileName);

                long length;
                try
                {
                    length = new System.IO.FileInfo(newPath).Length;
                }
                catch
                {
                    length = 0;
                }

                XmlNode save = xdoc.CreateElement("Save");
                XmlNode name = xdoc.CreateElement("Name");
                XmlNode FileSource = xdoc.CreateElement("FileSource");
                XmlNode DestPath = xdoc.CreateElement("destPath");
                XmlNode Time = xdoc.CreateElement("time");
                XmlNode Size = xdoc.CreateElement("size");
                XmlNode transferTime = xdoc.CreateElement("transferTime");
                XmlNode timeForCrypt = xdoc.CreateElement("cryptTime");


                name.InnerText = Name;
                FileSource.InnerText = newPath;
                DestPath.InnerText = $@"{destPath}\{Path.GetFileName(newPath)}";
                Time.InnerText = ts.ToString();
                Size.InnerText = length.ToString();
                transferTime.InnerText = elapsedTime;
                timeForCrypt.InnerText = cryptTimeString;



                save.AppendChild(name);
                save.AppendChild(FileSource);
                save.AppendChild(DestPath);
                save.AppendChild(Time);
                save.AppendChild(Size);
                save.AppendChild(transferTime);
                save.AppendChild(timeForCrypt);


                xdoc.DocumentElement.AppendChild(save);
                xdoc.Save(fileName);
            }
        }
    }
}
