using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._1_TaskProgramming._2_CancellingTasks
{
    internal class CancellingTasks
    {
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                        // 4.Cancel flag triggered from cts.Cancel();
                        //break;                    
                        //throw new OperationCanceledException();
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t"); //inf. loop                    
                }
            }, token);

            // 1.Loop will run infinetely
            t.Start();//0, 1, 2, ..., inf.
            
            // 2.Hold until press any key
            Console.ReadKey();
            // 3.Trigger cancel flag
            cts.Cancel();
            
            Console.WriteLine("CancellingTasks program done.");
            Console.ReadKey();
        }
    }
}
