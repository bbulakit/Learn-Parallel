using Learn_Parallel._1_TaskProgramming._1_TasksCreatingAndStarting;
using Learn_Parallel._1_TaskProgramming._2_CancellingTasks;
using Learn_Parallel._1_TaskProgramming._3_WaitingForTimeToPass;
using Learn_Parallel._1_TaskProgramming._4_WaitingForTasks;
using Learn_Parallel._1_TaskProgramming._5_ExceptionHandling;
using Learn_Parallel._2_DataSharing_Synchronization._1_CriticalSections;
using Learn_Parallel._2_DataSharing_Synchronization._2_InterlockedOperations;
using Learn_Parallel._2_DataSharing_Synchronization._3_SpinLocking_LockRecursion;
using Learn_Parallel._2_DataSharing_Synchronization._4_Mutex;
using Learn_Parallel._2_DataSharing_Synchronization._5_Reader_WriterLocks;
using Learn_Parallel._3_ConcurrentCollections._1_ConcurrentDictionary;
using Learn_Parallel._3_ConcurrentCollections._2_Producer_ConsumerCollections;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Demo
{
    internal static class Program
    {
        public static void Main()
        {
            #region "1_TaskProgramming"            
            //TaskCreatingAndStarting.Start();
            //CancellingTasks.Start();
            //CancellingTasks.Start2();
            //WaitingForTimeToPass.Start();
            //WaitingForTasks.Start();
            //ExceptionHandling.Start();
            #endregion

            #region "2_DataSharing&Synchronization"
            //CriticalSections.Start();
            //InterlockedOperations.Start();
            //SpinLocking.Start();
            //LockRecursion.Start(); //Showing SpinLock doesn't support Lock Recursion --> Dangerous
            //MutexExample.Start();
            //GlobalMutexExample.Start(); //Need open application .exe 2 instances
            //ReaderWriterExample.Start();
            #endregion

            #region "3_ConcurrentCollections"
            //ConcurrentDictionaryExample.Start();
            //ConcurrentQueueExample.Start();
            //ConcurrentQueueExample.Start2(); //Implement with Task.Run()
            ConcurrentStackExample.Start();
            #endregion
        }
    }
}