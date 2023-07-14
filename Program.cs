using System;
using System.Threading.Tasks;

namespace Demo
{
    internal static class Program
    {
        public static void Write(char c)
        {
            int i = 1_000;
            while(i-->0)
            {
                Console.Write(c);
            }
        }
        public static void Main()
        {
            Task.Factory.StartNew(() => Write('*')); //Thread1

            var t = new Task(() => Write('?')); //Thread2
            t.Start();

            Write('-'); //Main Thread

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}