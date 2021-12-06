using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyzerLib;
using TextAnalyzerLib.Interfaces;
using Xunit;

namespace UnitTest.TextAnalizerLibraryTest
{
    public class TextAnalyzer_Test
    {
        [Theory]
        [InlineData("abc 4564564 345 а3 ab345345 53abc45645 abcabc", 3, "abc", 4)]
        [InlineData("abc trer&&++__~~ 345 а3 ab34hgjg45 53abc45645jklhkjklabcabchljhl", 3, "abc", 4)]
        [InlineData("www 4564www564 345 а3 ww3453w45 53www45645 wwwwwww", 3, "www", 8)]
        [InlineData("www 4564wwwfjff 345fgjfgjа3 ww3453w45***@@@@@$$$$$%%%553www45645_\\|\\/wwwwwww", 3, "www", 8)]
        [InlineData("Ать-два, левой-правой, ать-два, левой-правой, Вдоль плетней, заборов и оград, " +
            "Ать-два, левой-правой, ать-два, левой-правой, Марширует бравый наш отряд.",              3, "два", 4)]
        public void CalculateSequenceFrequency_Test(string text, byte length, string expected_1, int expected_2)
        {
            TextAnalyzer textAnalyzer = new TextAnalyzer();
            string[] str = new string[1];
            str[0] = text;
            ConcurrentDictionary<string, int> result = textAnalyzer.CalculateSequenceFrequences(str, length);
            Assert.True(result.ContainsKey(expected_1) && result[expected_1] == expected_2);
        }

        [Theory]
        [InlineData("Ать-два, левой-правой, ать-два, левой-правой, Вдоль плетней, заборов и оград, " +
            "Ать-два, левой-правой, ать-два, левой-правой, Марширует бравый наш отряд.", 3, 2, "8 вой, 5 рав.")]
        public void GetMostPopularLetterLiterals_Test(string text, byte length, int top, string expected)
        {
            TextAnalyzer textAnalyzer = new TextAnalyzer();
            string[] str = new string[1];
            str[0] = text;
            IEnumerable<KeyValuePair<string, int>> keyValuePairs = textAnalyzer.
                CalculateSequenceFrequences(str, length).
                OrderByDescending(t => t.Value).Take(top);
            string mostPopular = string.Empty;
            int counter = 1;
            foreach(KeyValuePair<string, int> keyValuePair in keyValuePairs)
            {
                mostPopular += keyValuePair.Value + " " + keyValuePair.Key;
                if(counter < top)
                {
                    mostPopular += ", ";
                }
                else
                {
                    mostPopular += ".";
                }
                counter++;
            }

            Assert.Equal(expected, mostPopular);
        }
    }
}