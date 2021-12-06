using Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyzerLib.Interfaces;

namespace TextAnalyzerLib
{
    /// <summary>
    /// Text analyzer
    /// </summary>
    public class TextAnalyzer : ITextAnalyzer
    {
        public TextAnalyzer()
        {
        }

        /// <summary>
        /// Get most popular letter sequence.
        /// </summary>
        /// <param name="text">Text for analyze</param>
        /// <param name="length"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, int>> GetMostPopularLetterLiterals(string[] text, byte length, int top)
        {
            ConcurrentDictionary<string, int> frequences = CalculateSequenceFrequences(text, length);
            return frequences.GetTopPairsOrderedByValue(top, true);
        }

        /// <summary>
        /// Calculates frequences of all possible fixed size letter sequences (literals) in words.
        /// </summary>
        /// <param name="length">Length of literal</param>
        /// <param name="text">Text for analyze</param>
        /// <returns>Frequences of fixed size sequences</returns>
        public ConcurrentDictionary<string, int> CalculateSequenceFrequences(string[] text, byte length)
        {
            ConcurrentDictionary<string, int> frequences = new ConcurrentDictionary<string, int>();
            Parallel.ForEach(text, row => CalculateString(row, length, frequences));

            return frequences;
        }

        public void CalculateString(string row, byte length, ConcurrentDictionary<string, int> frequences)
        {
            Queue<char> literal = new Queue<char>();
            for (int i = 0; i < row.Length; i++)
            {
                char currentChar = row[i];

                //If only letters expected miss non letter symbols and reset literal
                if (!char.IsLetter(currentChar))
                {
                    literal.Clear();
                    continue;
                }

                //Else no nonletter chars, increment literal and tail loss that more than expected length
                literal.Enqueue(currentChar);
                if (literal.Count > length)
                {
                    literal.Dequeue();
                }

                if (literal.Count == length)
                {
                    string str = new string(literal.ToArray());
                    if (frequences.TryGetValue(str, out _))
                    {
                        frequences[str]++;
                    }
                    else
                    {
                        frequences.TryAdd(str, 1);
                    }
                }
            }
        }
    }
}