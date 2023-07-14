using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._2_DataSharing_Synchronization._4_Mutex
{
    internal class GlobalMutexExample
    {
        public static void Start()
        {
            //Open 2 applcation .exe which invoke this method.
            //First time open .exe the message "We can run the program just fine." will show up.
            //Don't close first .exe
            //Then open another same .exe name, the message "Sorry, {appName} is already running." will show up.
            GlobalMutex();

            Console.WriteLine("All done here.");
        }
        private static void GlobalMutex()
        {
            const string appName = "MyApp";
            Mutex mutex;
            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running.");
                return;
            }
            catch (WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program just fine.");
                // first arg = whether to give current thread initial ownership
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
        }
    }
}
