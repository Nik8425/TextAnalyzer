using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Extension
{
    public static class DictionaryExtension
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> GetTopPairsOrderedByValue<TKey, TValue>
            (this ConcurrentDictionary<TKey, TValue> dictionary, int top, bool descending = false)
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
    }
}
