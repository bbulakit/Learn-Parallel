using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._2_DataSharing_Synchronization._3_SpinLocking_LockRecursion
{
    internal class LockRecursion
    {
        static SpinLock sl = new SpinLock();
        public static void Start()
        {
            //It's very difficult to control who took a lock.
            //Avoid using SpinLock recursively.
            FuncLockRecursion(5);
        }

        public static void FuncLockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {

                Console.WriteLine("Exception" + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    FuncLockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
                        
        }
    }
}
