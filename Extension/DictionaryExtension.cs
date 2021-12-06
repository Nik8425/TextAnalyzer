using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Extension
{
    public static class DictionaryExtension
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> GetTopPairsOrderedByValue<TKey, TValue>
            (this Dictionary<TKey, TValue> dictionary, int top, bool descending = false)
        {
            if (descending)
            {
                return dictionary.OrderByDescending(t => t.Value).Take(top);
            }
            else
            {
                return dictionary.OrderBy(t => t.Value).Take(top);
            }
        }

        public static void Sort<TKey, TValue>(this Dictionary<TKey, TValue> dict, bool byValue = false, bool descending = false)
        {
            Dictionary<TKey, TValue> temp;
            if (descending)
            {
                temp = byValue ? dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value) : dict.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            }
            else
            {
                temp = byValue ? dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value) : dict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            }
            dict.Clear();
            foreach (var pair in temp)
            {
                dict.Add(pair.Key, pair.Value);
            }
        }
    }
}
