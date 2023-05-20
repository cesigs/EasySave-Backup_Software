using Newtonsoft.Json;

namespace GUIProject
{
    public class ExecuteSaveOnCreation
    {
        /// <summary>
        /// Execute the save as a complete save right after it's creation
        /// Will not write on logs or on states
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
                                Console.WriteLine($"Error can't find source of {post.Name}");
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Error can't find source of {post.Name}");
                        }
                    }
                }
            }
        }
    }
}
