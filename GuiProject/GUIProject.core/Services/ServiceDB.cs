using System.Text.Json;
using Newtonsoft.Json;

namespace GUIProject
{
    public class ServiceDB
    {
        /// <summary>
        /// Retrive the list of all the saveworks
        /// </summary>
        /// <returns></returns>
        public List<SaveWork> GetAll()
        {
            return Repository.Instance().SaveWorks;
        }

        /// <summary>
        /// Add a savework to our list
        /// </summary>
        /// <param name="work"></param>
        public void Add(SaveWork work)
        {
            Repository.Instance().SaveWorks.Add(work);
        }

        /// <summary>
        /// Get the list of all the saveworks from a database in json
        /// </summary>
        public void GenerateSaveWork()
        {
            string fileName = @"c:\bdd.json";
            string justText = File.ReadAllText(fileName);
            var myPosts = JsonConvert.DeserializeObject<SaveWork[]>(justText);
            foreach (var post in myPosts)
            {
                SaveWork savework = new SaveWork
                {
                    id = post.id,
                    Name = post.Name,
                    FileSource = post.FileSource,
                    destPath = post.destPath,
                    type = post.type,
                    time = post.time
                };
                new ServiceDB().Add(savework);
            };
        }

        /// <summary>
        /// Add a savework to our database in json
        /// </summary>
        /// <param name="savework"></param>
        public void WriteSaveWork(SaveWork savework)
        {
            string fileName = @"c:\bdd.json";
            string justText = File.ReadAllText(fileName);
            List<SaveWork> myPosts = JsonConvert.DeserializeObject<List<SaveWork>>(justText);
            int State = 0;
            myPosts.Add(savework);
            string json = System.Text.Json.JsonSerializer.Serialize(myPosts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
            new ServiceDB().Add(savework);
        }

    }
}
