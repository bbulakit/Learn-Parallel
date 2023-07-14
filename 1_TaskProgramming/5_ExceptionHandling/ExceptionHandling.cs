using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._1_TaskProgramming._5_ExceptionHandling
{
    internal class ExceptionHandling
    {
        public static void Start()
        {
            var t = Task.Factory.StartNew( () =>{
                throw new InvalidOperationException("Can't do this!") { Source = "t2" };
            });

            var t2 = Task.Factory.StartNew(() => {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {                
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.Write($"Exception {e.GetType()} from {e.Source}");
            }            


            Console.WriteLine("ExceptionHadling Program done.");
            Console.ReadKey();
        }
    }
}
