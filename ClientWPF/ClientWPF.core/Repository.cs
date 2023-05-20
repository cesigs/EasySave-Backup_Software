using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.core
{
    public class Repository
    {
        private static Repository _instance = null;
        public List<SaveWork> SaveWorks { get; set; }

        private Repository()
        {
            SaveWorks = new List<SaveWork>();
        }

        public static Repository Instance()
        {
            if (_instance == null)
                _instance = new Repository();
            return _instance;
        }
    }
}
