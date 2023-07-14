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

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0) Console.Write(o);
        }
        
        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }

        public static void Main()
        {
            string text1 = "testing", text2 = "this";

            var task1 = new Task<int>(TextLength, text1);
            task1.Start();

            Task.Factory.StartNew<int>(() => TextLength(text2));
            
            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}