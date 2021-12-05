using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Interfaces
{
    public interface IDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
