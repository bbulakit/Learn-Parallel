using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._1_TaskProgramming._3_WaitingForTimeToPass
{
    internal class WaitingForTimeToPass
    {
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            
            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);// Waiting for 5 seconds
                Console.WriteLine(cancelled ? "Bomb disarmed." : "Boom!!");
            }, token);

            t.Start();
            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Waiting for time to pass program done.");
            Console.ReadKey();
        }
    }
}
