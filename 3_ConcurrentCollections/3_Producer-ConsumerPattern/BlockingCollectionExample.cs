using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._3_ConcurrentCollections._3_Producer_ConsumerPattern
{
    internal class BlockingCollectionExample
    {
        static BlockingCollection<int> messages = 
            new BlockingCollection<int>(new ConcurrentBag<int>(), 10); //10 = messages capacity for blocking

        static CancellationTokenSource cts = new CancellationTokenSource();
        static Random random = new Random();

        public static void Start()
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);
            Console.ReadKey();
            cts.Cancel();
        }
        
        public static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] {consumer, producer },cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);                    
            }
        }

        private static void RunConsumer()
        {
            foreach(var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            //Producer will not produce more than 10 items as it is the capacity of the blocking collection
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(100));
            }
        }
    }
}
