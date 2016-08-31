using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            CodeEditors = new ObservableCollection<CodeEditorViewModel>();
        }

        public async Task RepopulateTree()
        {
            await Task.Factory.StartNew(new Action(async () =>
            {
                var allFiles = DirectoryTreeGenerator.GetFilesAndFoldersRecursively(_options.TaskDefinitionPaths);

                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.FileOrFolderEntries = new ObservableCollection<FileOrFolderEntry>(allFiles);
                }));
            }));
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

        private ObservableCollection<FileOrFolderEntry> _fileOrFolderEntries;
        public ObservableCollection<FileOrFolderEntry> FileOrFolderEntries
        {
            get { return _fileOrFolderEntries; }
            set { SetProperty(ref _fileOrFolderEntries, value); }
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
