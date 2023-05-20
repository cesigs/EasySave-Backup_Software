using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class View
    {
        /// <summary>
        /// Asks the user for his language preference
        /// </summary>
        private string _language;

        public string Language { get { return _language; } set { _language = value; } }

        public View() 
        {
            _language = string.Empty;
            GetLanguage();
        }

        private void GetLanguage()
        {
            Console.WriteLine("Tapez FR pour avoir le software en francais or EN to have the english version of the software");
            Language = Console.ReadLine();
        }
    }
}
