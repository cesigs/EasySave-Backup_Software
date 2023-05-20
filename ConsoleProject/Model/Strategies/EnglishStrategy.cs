using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// Contain the dictionary for the english version off the software that will be applied if the user use the english strategy
    /// </summary>
    class EnglishStrategy : IStrategyLanguage
    {
        string saveName;
        public void BeginAlgorithmTXT()
        {
            Console.WriteLine("1-Execute a save work\n2-Add a save work\n3-Show existent save works");
        }

        public void ExecuteASaveTXT()
        {
            Console.WriteLine("1-Execute on all the saves\n2-Execute on a particular save");
        }
        public void AddASaveTXT()
        {
            Console.WriteLine("You've added a new save");
        }

        public void ExecuteOnAllTheSavesTXT()
        {
            Console.WriteLine("Save done on all works");
        }

        public void SaveNameChoiceTXT()
        {
            Console.WriteLine("Enter the id of the save work to execute");
        }

        public void ExecuteOnASpecificSaveTXT()
        {
            Console.WriteLine("You have executed a save on the save work");
        }

        public void ErrorBadChoiceTXT()
        {
            Console.WriteLine("Error not recognized choice");
        }
        public void ErrorTooMuchSaveTXT()
        {
            Console.WriteLine("Error, 5 save already exist");
        }
        public void ErrorWrongIdTXT()
        {
            Console.WriteLine("Error, wrong ID selected please try again");
        }
    }
}
