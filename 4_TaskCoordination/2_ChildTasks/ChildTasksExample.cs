using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._4_TaskCoordination._2_ChildTasks
{
    internal class ChildTasksExample
    {
           public static void Start()
        {
            var parent = new Task(() =>
            {
                // detached = just a subtask within a task
                // no relationship

                // attached

                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting...");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finished.");

                    // comment Exception = completionHandler
                    // uncomment Exception = failHandler
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);
                
                //Run this task if main Child Task is failed
                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Unfortunately, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                //Run this task if main Child Task is completed
                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                child.Start();

                Console.WriteLine("Parent task starting...");
                Thread.Sleep(1000);
                Console.WriteLine("Parent task finished.");
            });

            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
            Console.ReadKey();
        }
    }
}
