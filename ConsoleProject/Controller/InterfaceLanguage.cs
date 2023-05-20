using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// Define all the different fonctions that will be used in the application
    /// of the pattern strategy for the languages.
    /// Actually can return english or french values
    /// </summary>
    internal interface IStrategyLanguage
    {
        void BeginAlgorithmTXT();
        void ExecuteASaveTXT();
        void AddASaveTXT();
        void ExecuteOnAllTheSavesTXT();
        void ExecuteOnASpecificSaveTXT();
        void SaveNameChoiceTXT();
        void ErrorBadChoiceTXT();
        void ErrorTooMuchSaveTXT();
        void ErrorWrongIdTXT();
    }
}
