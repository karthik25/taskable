using System;
using System.Collections.ObjectModel;
using System.Linq;
using TaskableApp.Models;

namespace TaskableApp.ViewModels
{
    public class CodeCollectionViewModel : BindableBase
    {
        private MainWindowViewModel _mainViewModel;

        public GenericCommand NewDocumentCommand
        {
            get; set;
        }

        public GenericCommand<CodeEditorViewModel> CloseDocumentCommand
        {
            get;set;
        }

        public CodeCollectionViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            NewDocumentCommand = new GenericCommand((Action) CreateNewDocument);
            CloseDocumentCommand = new GenericCommand<CodeEditorViewModel>(CloseDocument);
            FileOrFolderEntries = new ObservableCollection<FileOrFolderEntry>(DirectoryTreeGenerator.GetFilesAndFoldersRecursively(_options.TaskDefinitionPaths));
            CodeEditors = new ObservableCollection<CodeEditorViewModel>();
        }

        public void AddDocument(string filePath)
        {
            if (CodeEditors.Any(c => c.CurrentFile == filePath))
                return;
            var codeEditorModel = new CodeEditorViewModel(filePath, _mainViewModel);
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
            var newEditorModel = new CodeEditorViewModel(_mainViewModel);
            CodeEditors.Add(newEditorModel);
            CurrentDocument = newEditorModel;
        }

        public void CloseDocument(CodeEditorViewModel model)
        {
            CodeEditors.Remove(model);
            if (CodeEditors.Count == 0)
            {
                CreateNewDocument();
            }
            CurrentDocument = CodeEditors.LastOrDefault();            
        }
    }
}
