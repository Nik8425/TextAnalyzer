﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzerLib.Interfaces
{
    public interface ITextAnalyzer
    {
        public Dictionary<string, int> CalculateSequenceFrequences(string[] text, byte length);

        public IEnumerable<KeyValuePair<string, int>> GetMostPopularLetterLiterals(string[] text, byte length, int top);
    }
}