using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// Define the function that will be used in the strategy pattern to 
    /// Execute the save of all the save works or only one save work
    /// </summary>
    internal interface IStrategySaveType
    {
        void ExecuteSave();
    }
}
