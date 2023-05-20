using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Controller
{   /// <summary>
    /// Call the View to make the user chose the language he want to use.
    /// According to the input chose the strategy of the language to adopt
    /// </summary>
    class SelectLanguage
    {
        private View _view;
        private Context _context;
        public SelectLanguage()
        {
            _view = new View();
            _context = new Context();
            if (_view.Language == "FR" || _view.Language == "fr")
            {
                //If user selects FR, apply french language
                _context.SetStrategy(new FrenchStrategy());
                LaunchProgram launchProgram = new LaunchProgram(_context);
            }
            else if (_view.Language == "EN" || _view.Language == "en")
            {
                //If user selects EN, apply english language
                _context.SetStrategy(new EnglishStrategy());
                LaunchProgram launchProgram = new LaunchProgram(_context);
            }
            else
            {
                //If user selects wrongly, send an error and ask again for language selection
                Console.WriteLine("Erreur mauvaise saisie de langage\nError bad language selection");
            }
        }
    }
}
