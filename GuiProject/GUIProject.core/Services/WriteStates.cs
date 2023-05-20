using GUIProject.core;
using Newtonsoft.Json;
using System.Text.Json;

namespace GUIProject
{
    public class WriteStates
    {
        /// <summary>
        /// Allows to write the states in JSON in real time using the parameters given by executing on or all the saveworks
        /// Calculate the size left to save and return the percentage of execution
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="newPath"></param>
        /// <param name="destPath"></param>
        /// <param name="totalFiles"></param>
        /// <param name="totalSize"></param>
        /// <param name="dirSize"></param>
        /// <param name="filesLeft"></param>
        /// <param name="state"></param>
        public static void WriteStatesOnJson(string Name, string newPath, string destPath, int totalFiles, long totalSize, long dirSize, int filesLeft, string state)
        {
            string fileName = @"c:\states.json";
            if (System.IO.File.Exists(fileName))
            {
                string justText = File.ReadAllText(fileName);
                List<States> myPosts = JsonConvert.DeserializeObject<List<States>>(justText);
                States states = new States();
                states.Name = Name;
                myPosts.RemoveAll((x) => x.Name == states.Name);
                states.FileSource = newPath;
                states.destPath = destPath;
                states.totalFiles = totalFiles;
                states.totalSize = totalSize;
                states.filesLeft = filesLeft;
                states.sizeLeft = dirSize;
                states.time = DateTime.Now.ToString();
                states.state = state;
                states.progressPercentage = 100 - ((states.sizeLeft / states.totalSize) * 100);
                myPosts.Add(states);

                string json = System.Text.Json.JsonSerializer.Serialize(myPosts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
        }
    }
}
