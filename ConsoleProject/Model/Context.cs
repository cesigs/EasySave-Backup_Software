using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    /// <summary>
    /// The context allows us to define and the set the differents strategies  to use like the language or the save type that will be used
    /// </summary>
    class Context
    {
        public IStrategyLanguage _strategyLanguage;
        public IStrategySaveType _strategySaveType;

        public void SetStrategy(IStrategyLanguage strategyLanguage)
        {
            _strategyLanguage = strategyLanguage;
        }

        public void SetStrategySaveType(IStrategySaveType strategySaveType)
        {
            _strategySaveType = strategySaveType;
        }
    }
}
