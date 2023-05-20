using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class Repository
    {
        /// <summary>
        /// Define the instance of the class Repository(singleton)
        /// </summary>
        private static Repository _instance = null;

        /// <summary>
        /// Constructor of a list of saveworks
        /// </summary>
        public List<SaveWork> SaveWorks { get; set; }

        /// <summary>
        /// Assign a savework to the list of saveworks
        /// </summary>
        private Repository()
        {
            SaveWorks = new List<SaveWork>();
        }

        /// <summary>
        /// Controle the acces to the singleton instance
        /// </summary>
        public static Repository Instance()
        {
            if (_instance == null)
                _instance = new Repository();
            return _instance;
        }
    }
}
