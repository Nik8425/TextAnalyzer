using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAnalyzerLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            //Get file path
            string path = TryGetPathOrExit();

            //Analyze file
            var watch = System.Diagnostics.Stopwatch.StartNew();
            DocOperator docOperator = new DocOperator();
            string[] lines = await docOperator.ReadAllLinesAsync(path, Encoding.UTF8);
            TextAnalyzer textAnalyzer = new TextAnalyzer();
            IEnumerable<KeyValuePair<string, int>> mostPopular = await textAnalyzer.GetMostPopularLetterLiteralsAsync(lines, 3, 10);

            //Display results
            foreach (KeyValuePair<string, int> keyValuePair in mostPopular)
            {
                Console.WriteLine("\"" + keyValuePair.Key + "\"" + " becomes " + keyValuePair.Value + " times");
            }

            //Measure elapsed time
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Elapsed " + elapsedMs + " milliseconds.");
        }

        private static string TryGetPathOrExit()
        {
            string path = string.Empty;
            string message = "Enter the file path or press N for exit.";
            while (!File.Exists(path))
            {
                Console.WriteLine(message);
                path = Console.ReadLine();

                if (path.ToLower() == "n")
                {
                    Environment.Exit(0);
                }

                if (!File.Exists(path))
                {
                    message = "Incorrect file path. Enter the file path or press N for exit.";
                    Console.WriteLine(message);
                    path = Console.ReadLine();
                    if (path.ToLower() == "n")
                    {
                        Environment.Exit(0);
                    }
                }
            }

            return path;
        }
    }
}