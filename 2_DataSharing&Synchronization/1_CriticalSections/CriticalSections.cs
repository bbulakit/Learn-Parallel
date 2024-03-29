﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._2_DataSharing_Synchronization._1_CriticalSections
{
    internal static class CriticalSections
    {       
        public static void Start()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j=0; j< 1000;j++)
                    {
                        ba.Deposit(100);
                    }                    
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.WithDraw(100);
                    }                    
                }));                
            }
            
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }
    }
    public class BankAccount
    {
        public object padLock = new object();
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
            // += ; not atomic operation
            // op1: temp <- get_Balance() + amount
            // op2: set_Balance(temp)
            lock (padLock)
            {
                Balance += amount;
            }            
        }
        public void WithDraw(int amount)
        {            
            lock (padLock)
            {                          
                Balance -= amount;
            }
        }
    }
}
