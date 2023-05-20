using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class LaunchProgram
    {
        /// <summary>
        /// Controller of the Program
        /// Will assure the communication between the view and the models
        /// Is responsible to call the right method when it's necessary
        /// Execute the verification and chose which case apply
        /// </summary>
        /// <param name="_context"></param>
        public LaunchProgram(Context _context)
        {
            int i = 0;
            int j = 0;
            while (i==0) { 
            DBjson dbjson = new DBjson();
            _context._strategyLanguage.BeginAlgorithmTXT();
            string choice = Console.ReadLine();
                {
                    if (choice == "1")
                    // If user chooses 1, he will execute a save
                    {
                        i = 1;
                        while (j == 0)
                        {
                            _context._strategyLanguage.ExecuteASaveTXT();
                            string choiceNumberOfSave = Console.ReadLine();
                            if (choiceNumberOfSave == "1")
                            {
                                j = 1;
                                _context.SetStrategySaveType(new AllTheSavesStrategy());
                                _context._strategySaveType.ExecuteSave();
                                _context._strategyLanguage.ExecuteOnAllTheSavesTXT();
                            }
                            else if (choiceNumberOfSave == "2")
                            {
                                _context._strategyLanguage.SaveNameChoiceTXT();
                                _context.SetStrategySaveType(new SpecificSaveStrategy());
                                _context._strategySaveType.ExecuteSave();
                                _context._strategyLanguage.ExecuteOnASpecificSaveTXT();
                                j = 1;
                            }
                            else
                            {
                                j = 0;
                                _context._strategyLanguage.ErrorBadChoiceTXT();
                            }
                        }
                        
                    }
                    else if (choice == "2")
                    //If user chooses 2, he will add a save work
                    {
                        i = 1;
                        while (j == 0)
                        {
                            if (dbjson.addNewSaveWork() == null)
                            {
                                _context._strategyLanguage.ErrorTooMuchSaveTXT();
                                j = 0;
                                i = 0; 
                                break;
                            }
                            else
                            {
                                _context._strategyLanguage.AddASaveTXT();
                                _context.SetStrategySaveType(new ExecuteSaveOnCreation());
                                _context._strategySaveType.ExecuteSave();
                                j = 1;
                            }
                        }

                    }
                    else if (choice == "3")
                    //If user chooses 3, he will see all the save work made
                    {
                        i = 1;
                        dbjson.displaySaveWorks();
                    }
                    else
                    //If user chooses wrongly, the program will send an error message and will restart 
                    {
                        i = 0;
                        _context._strategyLanguage.ErrorBadChoiceTXT();
                    }
                }
            }
        }
    }
}
