using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._1_TaskProgramming._4_WaitingForTasks
{
    internal class WaitingForTasks
    {
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");
                for(int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);                    
                }

                Console.WriteLine("I'm done.");                                
            }, token);
            t.Start();

            //t.Wait(token); //wait for the task as long as it takes

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            //Console.ReadKey();
            //cts.Cancel();

            //Task.WaitAll(t, t2); //I take 5 sec -> I'm done -> Waiting for tasks program done.
            //Task.WaitAny(t, t2); //I take 5 sec -> Waiting for tasks program done. -> I'm done.

            /*  < 3000  t2.status will show 'Running'
             *  > 3000 t2.status will show "Ran to Completion"
             *  > 5000 t1.status will show "Ran to completion" else "Running"
             */
            //Task.WaitAll(new[] { t, t2 }, 1000, token);   //1000 = timeout     
            Task.WaitAll(new[] { t, t2 }, 4000, token);   //4000 = timeout            

            Console.WriteLine($"Task t status is {t.Status}");      //Running
            Console.WriteLine($"Task t2 status is {t2.Status}"); //Ran to completion // complete execution            

            Console.WriteLine("Waiting For Tasks program done.");
            Console.ReadKey();

            Console.ReadKey();
       }
    }
}
