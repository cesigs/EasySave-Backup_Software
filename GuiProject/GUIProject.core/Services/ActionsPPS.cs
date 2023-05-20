using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUIProject
{
    public class ActionsPPS
    {
        /// <summary>
        /// Allows to pause the execution of the threads that are executing the save works by using manualResetEvent
        /// </summary>
        /// <param name="manualResetEvent"></param>
        public void Play(ManualResetEvent manualResetEvent)
        {
            manualResetEvent.Set();
        }

        /// <summary>
        /// Allows to play the execution of the threads that are executing the save works by using manualResetEvent
        /// </summary>
        /// <param name="manualResetEvent"></param>
        public void Pause(ManualResetEvent manualResetEvent)
        {
            manualResetEvent.Reset();
        }

        /// <summary>
        /// Stop the running threads using a list of threads and then empty this list
        /// </summary>
        /// <param name="threadList"></param>
        public void Stop(IList<Thread> threadList)
        {
            if (threadList != null)
            {
                foreach (Thread thread in threadList)
                {
                    thread.Interrupt();
                }
            }
            threadList.Clear();
        }
    }
}
