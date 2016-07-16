using System.Collections.Generic;
using TaskableApp.Abstract;

namespace TaskableApp.Models
{
    public class FileOrFolderEntry : INamedFileEntry
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsFolder { get; set; }
        public List<FileOrFolderEntry> Entries { get; set; }
    }   
}
