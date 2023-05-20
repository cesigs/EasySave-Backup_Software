using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// Inheritance class  of the interface IStrategySaveType to use ExecuteSave()
    /// </summary>
    class ExecuteSaveOnCreation : IStrategySaveType
    {
        /// <summary>
        /// Will only be executed when a save work is created
        /// Is obligatorly complete to have a first save functional
        /// </summary>
        public void ExecuteSave()
        {
            string fileName = @"c:\bdd.json";
            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                var myPosts = JsonConvert.DeserializeObject<SaveWork[]>(justText);
                foreach (var post in myPosts)
                {
                    if (post == myPosts.Last())
                    {
                        try
                        {
                            foreach (string dirPath in Directory.GetDirectories(post.FileSource, "*", SearchOption.AllDirectories))
                            {
                                Directory.CreateDirectory(dirPath.Replace(post.FileSource, post.destPath));
                            }
                            try
                            {
                                foreach (string newPath in Directory.GetFiles(post.FileSource, "*.*", SearchOption.AllDirectories))
                                {
                                    File.Copy(newPath, newPath.Replace(post.FileSource, post.destPath), true);
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Error cant find source of " + post.Name);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error cant find source of " + post.Name);
                        }
                    }
                }
            }
        }
    }
}
