using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._2_DataSharing_Synchronization._4_Mutex
{
    internal class MutexExample
    {
        public static void Start()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        //This ensures that only one thread can access the shared resource at a time
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            //assuming the lock was acquired successfully
                            if (haveLock) mutex.ReleaseMutex(); 
                        }                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        //This ensures that only one thread can access the shared resource at a time
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            //ba.WithDraw(100);
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            //assuming the lock was acquired successfully
                            if (haveLock) mutex2.ReleaseMutex();
                        }                        
                    }
                }));

                //Transfer
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
                
            }

                Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
            Console.WriteLine($"Final balance2 is {ba2.Balance}");
        }
    }
    public class BankAccount
    {
        public object padLock = new object();
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
                Balance += amount;
        }
        public void WithDraw(int amount)
        {
                Balance -= amount;
        }
        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }
}
