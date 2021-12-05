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
            // Main body here
            //string path = "C:\\Users\\Nik\\Desktop\\War and Peace\\txt\\TextFile.txt";
            //string path = "C:\\Users\\Nik\\source\\repos\\TextAnalyzer\\ConsoleApp\\NewFolder\\TextFileExample8.txt";
            string path = string.Empty;
            while (!File.Exists(path))
            {
                Console.WriteLine("Enter the file path.");
                path = Console.ReadLine();
            }

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

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Elapsed " + elapsedMs + " milliseconds.");
        }
    }
}