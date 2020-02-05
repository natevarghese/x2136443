using System;
using System.IO;

namespace x2136443.Services
{
    public interface IFileManager
    {
        string GetPathForFile(string filename);
        bool FileExists(string filename);
        bool SaveFile(string filename, byte[] bytes);
    }

    public class FileManager : IFileManager
    {
        string DefaultDirectory => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string GetPathForFile(string filename)
        {
            return Path.Combine(DefaultDirectory, filename);
        }

        public bool FileExists(string filename)
        {
            return File.Exists(GetPathForFile(filename));
        }
        public bool SaveFile(string filename, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(GetPathForFile(filename), bytes);
                return true;
            }
            catch //out of space or whatever
            {
                return false;
            }
        }
    }
}
