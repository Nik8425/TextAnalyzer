using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Interfaces
{
    public interface ITextStorage<T>
    {
        public T[] Text { get; }
    }
}