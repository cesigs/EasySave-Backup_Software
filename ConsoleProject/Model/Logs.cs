using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// Parent class of the differents logs functions
    /// </summary>
    public class Logs
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string destPath { get; set; }
        public string time { get; set; }
        public int size { get; set; }
        public string transferTime { get; set; }
    }
}
