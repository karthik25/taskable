using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskableApp.Models;

namespace TaskableApp
{
    public static class DirectoryTreeGenerator
    {
        public static List<FileOrFolderEntry> GetFilesAndFoldersRecursively(List<string> basePaths)
        {
            var fileOrFolderEntries = new List<FileOrFolderEntry>();
            foreach (var basePath in basePaths)
            {
                if (!Directory.Exists(basePath))
                    throw new System.Exception("Directory does not exist " + basePath);

                var fileOrFolderEntry = new FileOrFolderEntry { Name = GetDirectoryName(basePath), Path = basePath, IsFolder = true, Entries = new List<FileOrFolderEntry>() };
                fileOrFolderEntries.Add(fileOrFolderEntry);
                var directories = Directory.GetDirectories(basePath);
                foreach (var directory in directories)
                {
                    ProcessDirectory(directory, fileOrFolderEntry);
                }

                FindFilesInFolders(basePath, fileOrFolderEntry);
            }

            return fileOrFolderEntries;
        }

        private static void ProcessDirectory(string directory, FileOrFolderEntry item)
        {
            var currentDirectory = new FileOrFolderEntry { Name = GetDirectoryName(directory), Path = directory, IsFolder = true, Entries = new List<FileOrFolderEntry>() };
            item.Entries.Add(currentDirectory);

            var directories = Directory.GetDirectories(directory);

            foreach (var dir in directories)
            {
                ProcessDirectory(dir, currentDirectory);
            }

            FindFilesInFolders(directory, currentDirectory);
        }

        private static void FindFilesInFolders(string basePath, FileOrFolderEntry directoryRoot)
        {
            var allowedExtensions = new[] { ".cs", ".csx" };
            var files = Directory
                .GetFiles(basePath)
                .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                .ToList();
            foreach (var file in files)
            {
                directoryRoot.Entries.Add(new FileOrFolderEntry { Name = System.IO.Path.GetFileName(file), Path = file, IsFolder = false });
            }
        }

        private static string GetDirectoryName(string path)
        {
            return path.Substring(path.LastIndexOf(@"\") + 1);
        }
    }
}
