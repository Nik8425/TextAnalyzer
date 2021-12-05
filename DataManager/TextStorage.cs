using DataManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class TextStorage<T> : ITextStorage<T>
    {
        public T[] Text { get; private set; }

        public TextStorage()
        {
        }    
    }
}
