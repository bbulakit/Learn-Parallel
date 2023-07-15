using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._3_ConcurrentCollections._2_Producer_ConsumerCollections
{
    internal class ConcurrentQueueExample
    {
        public static void Start()
        {
            //Queue = FIFO
            var q = new ConcurrentQueue<int>();
            //producer
            q.Enqueue(1);
            q.Enqueue(2);

            // Queue: 2 1 <- front

            int result;
            //int last = q.Dequeue();
            //consumer
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            // Queue: 2

            //int peeked = q.Peek();
            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Last element is {result}");
            }
        }
        public static void Start2()
        {
            // Create a concurrent queue
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

            // Start a producer task
            Task producerTask = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Enqueue items to the queue
                    queue.Enqueue(i);
                    Console.WriteLine($"Produced: {i}");
                    Thread.Sleep(100);
                }
            });

            // Start a consumer task
            Task consumerTask = Task.Run(() =>
            {
                int item;
                while (queue.TryDequeue(out item))
                {
                    // Dequeue items from the queue
                    Console.WriteLine($"Consumed: {item}");
                    Thread.Sleep(200);
                }
            });

            // Wait for both tasks to complete
            Task.WaitAll(producerTask, consumerTask);

            Console.WriteLine("All tasks completed. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
