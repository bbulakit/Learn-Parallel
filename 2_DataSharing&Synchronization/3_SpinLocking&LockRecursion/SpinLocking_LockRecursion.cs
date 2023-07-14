﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._2_DataSharing_Synchronization._3_SpinLocking_LockRecursion
{
    internal class SpinLocking_LockRecursion
    {
        public static void Start()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
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
        private int balance;
        public int Balance { get => balance; private set => balance = value; }
        public void Deposit(int amount)
        {
            balance += amount;
        }
        public void WithDraw(int amount)
        {
            balance -= amount;
        }
    }
}