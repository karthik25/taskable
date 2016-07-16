using System;
using System.Collections.ObjectModel;
using System.Linq;
using TaskableApp.Models;

namespace TaskableApp.ViewModels
{
    public class CodeCollectionViewModel : BindableBase
    {
        public GenericCommand NewDocumentCommand
        {
            get; set;
        }

        public CodeCollectionViewModel()
        {
            NewDocumentCommand = new GenericCommand((Action) CreateNewDocument);
            FileOrFolderEntries = new ObservableCollection<FileOrFolderEntry>(DirectoryTreeGenerator.GetFilesAndFoldersRecursively(tempBasePath));
            CodeEditors = new ObservableCollection<CodeEditorViewModel>();
        }

        public void AddDocument(string filePath)
        {
            if (CodeEditors.Any(c => c.CurrentFile == filePath))
                return;
            var codeEditorModel = new CodeEditorViewModel(filePath);
            CodeEditors.Add(codeEditorModel);
            CurrentDocument = codeEditorModel;
        }

        public ObservableCollection<CodeEditorViewModel> CodeEditors
        {
            get; set;
        }

        private CodeEditorViewModel _currentDocument;
        public CodeEditorViewModel CurrentDocument
        {
            get { return _currentDocument; }
            set { SetProperty(ref _currentDocument, value); }
        }

        public ObservableCollection<FileOrFolderEntry> FileOrFolderEntries
        {
            get; set;
        }

        public void CreateNewDocument()
        {
            var newEditorModel = new CodeEditorViewModel();
            CodeEditors.Add(newEditorModel);
            CurrentDocument = newEditorModel;
        }

        private const string tempBasePath = @"C:\Users\Karthik\Custom\GitHub Forks\ILSpy";
    }
}
