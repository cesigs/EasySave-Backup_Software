using ConsoleProject;
using ConsoleProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveManager
{
    /// <summary>
    /// Begining point of the program
    /// Call the fist method to use, in this case the selection of the language
    /// </summary>
    class Program
    {
        /// <summary>
        /// This method permit to keep the same language during
        /// the execution of the program 
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                SelectLanguage selectLanguage = new SelectLanguage();
            }
            
        }
    }
}