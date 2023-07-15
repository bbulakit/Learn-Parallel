using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._4_TaskCoordination._1_Continuations
{
    internal class ContinuationsExample
    {
        public static void Start()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            });

            task2.Wait();
        }

        public static void Start2()
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            //ContinueWhenAny() //print Task1 or Task2
            //ContinueWhenAll() //print Task1 and Task2
            var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 }, tasks =>
            {
                Console.WriteLine("Tasks completed: ");
                foreach (var t in tasks)
                {
                    Console.WriteLine(" - " + t.Result);
                }
                Console.WriteLine("All tasks done");
            });
            task3.Wait();            
        }
    }
}
