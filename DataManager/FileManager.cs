using DataManager.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DataManager
{
    public class FileManager : IFileManager
    {
        public string GetStringFromFile(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] GetTextFromFile(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}