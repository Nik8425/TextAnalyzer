﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Interfaces
{
    public interface IFileManager
    {
        public byte[] GetTextFromFile(string path);
        public string GetStringFromFile(string path);
    }
}