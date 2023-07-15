using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_Parallel._3_ConcurrentCollections._1_ConcurrentDictionary
{
    internal class ConcurrentDictionaryExample
    {
        //The biggest reason to use ConcurrentDictionary over the normal Dictionary is thread safety
        //In case, multiple threads using the same dictionary at the same time
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();
        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");            
            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
            //Task {id} added the element.
            //Main thread did not add the element
        }
        public static void Start()
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();
                        
            capitals["Russia"] = "Leningrad";
            //capitals["Russia"] = "Moscow";
            //Console.WriteLine(capitals["Russia"]);//return Moscow

            capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + "--> Moscow" );
            Console.WriteLine($"The capital of Russia is {capitals["Russia"]}.");//Leningrad --> Moscow
            //if no captials["Russia"] before, above function will return only "Moscow" instead

            capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm"); //If cannot get, return Stockholm
            Console.WriteLine($"The capital of Sweden is {capOfSweden}.");//Upsala

            const string toRemove = "Russia";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove) Console.WriteLine($"We just removed {removed}");
            else Console.WriteLine($"Failed to remove the capital of {toRemove}");

            foreach(var kv in capitals)
            {
                Console.WriteLine($" - {kv.Value} is the capital of {kv.Key}");
                //No Russia 'cause we removed it before
            }
        }
    }
}
