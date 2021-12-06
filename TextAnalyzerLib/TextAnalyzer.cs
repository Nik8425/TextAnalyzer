using Extension;
using System;
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
            Dictionary<string, int> frequences = CalculateSequenceFrequences(text, length);
            return frequences.GetTopPairsOrderedByValue(top, true);
        }

        /// <summary>
        /// Calculates frequences of all possible fixed size letter sequences (literals) in words.
        /// </summary>
        /// <param name="length">Length of literal</param>
        /// <param name="text">Text for analyze</param>
        /// <returns>Frequences of fixed size sequences</returns>
        public Dictionary<string, int> CalculateSequenceFrequences(string[] text, byte length)
        {
            string str = string.Empty;
            Dictionary<string, int> frequences = new Dictionary<string, int>();
            Queue<char> literal = new Queue<char>();
            foreach (string row in text)
            {
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
                        str = new string(literal.ToArray());
                        int count = 0;
                        if (frequences.TryGetValue(str, out count))
                        {
                            frequences[str]++;
                        }
                        else
                        {
                            frequences.Add(str, 1);
                        }
                    }
                }
            }

            return frequences;
        }
    }
}