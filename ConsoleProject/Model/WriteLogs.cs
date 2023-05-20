using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleProject
{
    public class WriteLogs
    {
        /// <summary>
        /// This permits to write the logs on json
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="newPath"></param>
        /// <param name="destPath"></param>
        /// <param name="ts"></param>
        public static void WriteLogsOnJson(string Name, string newPath, string destPath, TimeSpan ts)
        {

            string elapsedTime = ts.ToString();

            string fileName = @"c:\logs.json";

            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                List<Logs> myPosts = JsonConvert.DeserializeObject<List<Logs>>(justText);
                Logs logs = new Logs();
                logs.Name = Name;
                logs.FileSource = newPath;
                logs.destPath = destPath + @"\" + Path.GetFileName(newPath);
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
                myPosts.Add(logs);
                string json = System.Text.Json.JsonSerializer.Serialize(myPosts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
        }

        public static void WriteLogsOnXML(string Name, string newPath, string destPath, TimeSpan ts)
        {
            string elapsedTime = ts.ToString();

            string fileName = @"c:\logs.xml";

            if (File.Exists(fileName))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileName); ;

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
                

                name.InnerText = Name;
                FileSource.InnerText = newPath;
                DestPath.InnerText = destPath + @"\" + Path.GetFileName(newPath);
                Time.InnerText = ts.ToString();
                Size.InnerText = length.ToString();
                transferTime.InnerText = elapsedTime;
                



                save.AppendChild(name);
                save.AppendChild(FileSource);
                save.AppendChild(DestPath);
                save.AppendChild(Time);
                save.AppendChild(Size);
                save.AppendChild(transferTime);


                xdoc.DocumentElement.AppendChild(save);
                xdoc.Save(fileName);
            }
        }
    }
}
