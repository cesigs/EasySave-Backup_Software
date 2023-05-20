using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleProject
{
    public class DBjson
    {
        public SaveWork displaySaveWorks()
        {
            // This method permit to display all the save work and their parameters from the json database
            string fileName = @"c:\bdd.json";

            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                var myPosts =   JsonConvert.DeserializeObject<SaveWork[]>(justText);

                foreach (var post in myPosts)
                {
                    Console.WriteLine($"{post.id} {post.Name} {post.FileSource} {post.destPath} {post.time}");
                }
            }
            return (null);
        }

        public SaveWork addNewSaveWork()
        {
            // This method permits to add a save work to the json database by asking 
            // the differents parameters
            string fileName = @"c:\bdd.json";

            string justText = File.ReadAllText(fileName);
            List<SaveWork> myPosts = JsonConvert.DeserializeObject<List<SaveWork>>(justText);
            int State = 0;
            if(myPosts.Count < 5)
                {
                SaveWork sw = new SaveWork();
                int count = myPosts.Count;
                sw.id = count + 1;

                Console.WriteLine("Nom / Name");
                sw.Name = Console.ReadLine();

                Console.WriteLine("Source");
                sw.FileSource = Console.ReadLine();

                Console.WriteLine("Destination");
                sw.destPath = Console.ReadLine();

                Console.WriteLine("Type: 1- Complete\n2-Differential/Differentielle");
                string type = Console.ReadLine();
                if (type == "1")
                {
                    sw.type = "complete";
                }
                else if (type == "2")
                {
                    sw.type = "differential";
                }
                else
                {
                    Console.WriteLine("ERROR");
                }

                sw.time = DateTime.Now.ToString();

                if (sw.type != null)
                {
                    myPosts.Add(sw);
                    string json = System.Text.Json.JsonSerializer.Serialize(myPosts, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(fileName, json);
                    return (sw);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
