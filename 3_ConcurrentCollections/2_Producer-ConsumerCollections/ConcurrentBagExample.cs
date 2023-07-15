using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._3_ConcurrentCollections._2_Producer_ConsumerCollections
{
    internal class ConcurrentBagExample
    {
        public static void Start()
        {
            //Bag No ordering

            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"Task:{Task.CurrentId} has added {i1}");

                    int result;
                    if (bag.TryPeek(out result)) Console.WriteLine($"Task:{Task.CurrentId} has peeked the value {result}");

                }));
            }
            Task.WaitAll(tasks.ToArray());

            int last;
            if(bag.TryTake(out last))
            {
                Console.WriteLine($"I got last {last}");
            }

        }
    }
}
