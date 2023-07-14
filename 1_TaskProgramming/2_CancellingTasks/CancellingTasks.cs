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

            //optional, register as subscribing the event.
            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested");
            });

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

            //optional, handle subscribed event after cancellation
            Task.Factory.StartNew(() => {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancellation was requested");
            });

            // 1.Loop will run infinetely
            t.Start();//0, 1, 2, ..., inf.
            
            // 2.Hold until press any key
            Console.ReadKey();
            // 3.Trigger cancel flag
            cts.Cancel();
            
            Console.WriteLine("CancellingTasks program done.");
            Console.ReadKey();
        }

        public static void Start2()
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token,
                preventative.Token,
                emergency.Token
                );

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t"); //inf. loop 
                    Thread.Sleep(1000);
                }
            }, paranoid.Token);

            Console.ReadKey();

            /*Same results */
            //emergency.Cancel();
            //preventative.Cancel();
            emergency.Cancel();

            Console.WriteLine("CancellingTask2 program done.(CreateLinkedTokenSource example)");
            Console.ReadKey();

        }

    }
}
