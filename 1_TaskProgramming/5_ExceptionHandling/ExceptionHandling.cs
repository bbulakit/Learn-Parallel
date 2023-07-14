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
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine($"Handled elsewhere: {e.GetType()} from {e.Source}");
            }

            Console.WriteLine("ExceptionHadling Program done.");
            Console.ReadKey();
        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t2" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae) //Represents one or more errors that occur during application execution.
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException) {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }return false;
                });
            }
        }
    }
}
