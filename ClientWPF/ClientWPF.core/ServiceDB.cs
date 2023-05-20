using ClientWPF.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    /// <summary>
    /// Class used to get and display the list of the save work
    /// </summary>
    public class ServiceDB
    {
        public List<SaveWork> GetAll()
        {
            return Repository.Instance().SaveWorks;
        }

        public void GenerateSaveWork(string messageFromServer)
        {
            var myPosts = JsonConvert.DeserializeObject<SaveWork[]>(messageFromServer);
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

        public void Add(SaveWork work)
        {
            Repository.Instance().SaveWorks.Add(work);
        }
    }
}
