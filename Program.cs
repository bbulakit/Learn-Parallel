using Learn_Parallel._1_TaskProgramming._1_TasksCreatingAndStarting;
using Learn_Parallel._1_TaskProgramming._2_CancellingTasks;
using Learn_Parallel._1_TaskProgramming._3_WaitingForTimeToPass;
using Learn_Parallel._1_TaskProgramming._4_WaitingForTasks;
using Learn_Parallel._1_TaskProgramming._5_ExceptionHandling;
using Learn_Parallel._2_DataSharing_Synchronization._1_CriticalSections;
using Learn_Parallel._2_DataSharing_Synchronization._2_InterlockedOperations;
using Learn_Parallel._2_DataSharing_Synchronization._3_SpinLocking_LockRecursion;
using System;
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
            SpinLocking_LockRecursion.Start();
            #endregion
        }
    }
}