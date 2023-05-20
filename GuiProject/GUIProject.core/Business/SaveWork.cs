using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    /// <summary>
    /// Parent class that defines the different save work function
    /// </summary>
    public class SaveWork
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string destPath { get; set; }
        public string type { get; set; }
        public string time { get; set; }
    }
}
