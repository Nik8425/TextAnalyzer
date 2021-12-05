using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzerLib.Interfaces
{
    public interface ITextAnalyzer
    {
        public Task<Dictionary<string, int>> CalculateSequenceFrequencesAsync(string[] text, byte length);

        public Task<IEnumerable<KeyValuePair<string, int>>> GetMostPopularLetterLiteralsAsync(string[] text, byte length, int top);
    }
}