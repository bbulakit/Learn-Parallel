using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._2_DataSharing_Synchronization._5_Reader_WriterLocks
{
    internal class ReaderWriterExample
    {

        static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim();
        static Random rand = new Random();
        public static void Start()
        {
            int x = 0;
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //padLock.EnterReadLock();                    
                    padLock.EnterUpgradeableReadLock();

                    if(i%2 == 0)
                    {
                        padLock.EnterWriteLock();
                        x = 123;
                        padLock.ExitWriteLock();
                    }

                    Console.WriteLine($"Entered read lock, x = {x}");
                    Task.Delay(5000).Wait();
                    //padLock.ExitReadLock();
                    padLock.ExitUpgradeableReadLock();
                    
                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>                
                {
                    Console.WriteLine(e);
                    return true;
                });                            
            }

            while (true)
            {
                Console.ReadKey();
                padLock.EnterWriteLock();
                Console.Write("Write lock acquired");
                int newValue = rand.Next(10);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padLock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }
            
        }        
        

    }
}
