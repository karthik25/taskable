using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TaskableApp.ViewModels
{
    public class CodeEditorViewModel : BindableBase
    {
        private MainWindowViewModel _mainViewModel;

        public GenericCommand SaveDocumentCommand
        {
            get; set;
        }

        public GenericCommand OpenInVsCommand
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string CurrentFile
        {
            get; set;
        }

        public IDocument Document
        {
            get; set;
        }

        private string _statusText;
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                SetProperty<string>(ref _statusText, value);
            }
        }

        private string _statusTextY;
        public string StatusTextY
        {
            get
            {
                return _statusTextY;
            }
            set
            {
                SetProperty<string>(ref _statusTextY, value);
            }
        }

        public CodeEditorViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            Title = "New Document";
            Document = new TextDocument(Properties.Resources.Sample);
            SaveDocumentCommand = new GenericCommand(Save);
            OpenInVsCommand = new GenericCommand((Action)OpenFileInVs);
        }

        public CodeEditorViewModel(string filePath, MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            if (File.Exists(filePath))
            {
                CurrentFile = filePath;
                Title = Path.GetFileName(filePath);
                Document = FileLoader.LoadFile(filePath);
                StatusText = filePath;
            }
            SaveDocumentCommand = new GenericCommand(Save);
            OpenInVsCommand = new GenericCommand((Action)OpenFileInVs);
        }

        private async Task Save()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();

                if (dialog.ShowDialog() == true)
                {
                    this.CurrentFile = dialog.FileName;
                }
            }

            this.StatusTextY = "Saving: " + this.CurrentFile;
            var contents = Document.Text;
            using (var writer = new StreamWriter(CurrentFile, false))
            {
                await writer.WriteAsync(contents);
            }
            this.StatusTextY = "Saved";
            PerformDelayedUpdate(() => { this.StatusTextY = ""; });

            _mainViewModel.TaskSelectorViewModel.TaskSaved();
        }

        private void PerformDelayedUpdate(Action action, int intervalSeconds = 5)
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(intervalSeconds);
            timer.Tick += new EventHandler((object s, EventArgs a) =>
            {
                action();
            });
            timer.Start();
        }

        public void OpenFileInVs()
        {
            var vsPath = @"C:\Program Files (x86)\Microsoft VS Code\code.exe";
            if (!string.IsNullOrEmpty(CurrentFile))
            {
                Process.Start(vsPath, CurrentFile);
            }
            else
            {
                MessageBox.Show("Please save the file first to edit it in VS Code.", "Please save the file!");
            }
        }

        private static class FileLoader
        {
            public static IDocument LoadFile(string filePath)
            {
                IDocument document;
                using (FileStream fs = new FileStream(filePath,
                                   FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = FileReader.OpenStream(fs, Encoding.UTF8))
                    {
                        document = new TextDocument(reader.ReadToEnd());
                    }
                }
                return document;
            }
        }
    }
}
