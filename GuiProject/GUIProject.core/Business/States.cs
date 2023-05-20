using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.core
{
    /// <summary>
    /// Parent class that defines the differents states functions
    /// </summary>
    public class States
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string destPath { get; set; }
        public int totalFiles { get; set; }
        public string time { get; set; }
        public double totalSize { get; set; }
        public double sizeLeft { get; set; }
        public int filesLeft { get; set; }
        public string state { get; set; }
        public double progressPercentage { get; set; }
    }
}
