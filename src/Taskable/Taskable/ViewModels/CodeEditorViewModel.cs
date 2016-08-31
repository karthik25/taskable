using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TaskableRoslynCore;
using TaskableRoslynCore.Analyzer;

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

        private Identifier _selectedIdentifier;
        public Identifier SelectedIdentifier
        {
            get { return _selectedIdentifier; }
            set { SetProperty(ref _selectedIdentifier, value); }
        }

        private Identifier _nextIdentifier;
        public Identifier NextIdentifier
        {
            get { return _nextIdentifier; }
            set { SetProperty(ref _nextIdentifier, value); }
        }

        private ObservableCollection<Identifier> _identifiers;
        public ObservableCollection<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { SetProperty(ref _identifiers, value); }
        }

        private bool _isNavVisible;
        public bool IsNavVisible
        {
            get { return _isNavVisible; }
            set { SetProperty(ref _isNavVisible, value); }
        }

        public GenericCommand NavCommand { get; set; }
        public GenericCommand DownArrowCommand { get; set; }
        public GenericCommand UpArrowCommand { get; set; }

        public event EventHandler<CodeEditorNavEventArgs> CaretPositionChanged;

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
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                GetIdentifierList();
            });
            this.NavCommand = new GenericCommand(() =>
            {
                this.IsNavVisible = !this.IsNavVisible;
            });
            this.DownArrowCommand = new GenericCommand(() =>
            {
                OnCaretPositionChanged(NavEventType.Down);
            });
            this.UpArrowCommand = new GenericCommand(() =>
            {
                OnCaretPositionChanged(NavEventType.Up);
            });
        }

        private void OnCaretPositionChanged(NavEventType eventType)
        {
            if (CaretPositionChanged != null)
            {
                var handler = CaretPositionChanged;
                handler(this, new CodeEditorNavEventArgs(eventType));
            }
        }

        public Identifier GetNextIdentifier(int caretOffset)
        {
            if (caretOffset < this.Identifiers.Min(i => i.OffsetStart))
                return this.Identifiers.FirstOrDefault();

            var currentIdentifier = this.Identifiers.Where(i => i.OffsetStart <= caretOffset)
                                                 .Where(i => i.Between(caretOffset))
                                                 .LeastDistance();
            this.NextIdentifier = this.Identifiers.FirstOrDefault(i => i.Index == (currentIdentifier.Index + 1));
            return this.NextIdentifier;
        }

        public Identifier GetPreviousIdentifier(int caretOffset)
        {
            if (caretOffset > this.Identifiers.Max(i => i.OffsetStart))
                return this.Identifiers.LastOrDefault();

            var currentIdentifier = this.Identifiers.Where(i => i.OffsetStart <= caretOffset)
                                                 .Where(i => i.Between(caretOffset))
                                                 .LeastDistance();
            this.NextIdentifier = this.Identifiers.FirstOrDefault(i => i.Index == (currentIdentifier.Index - 1));
            return this.NextIdentifier;
        }

        public void HideNav()
        {
            this.SelectedIdentifier = null;
            this.IsNavVisible = false;
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

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                GetIdentifierList();
            });
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

        public async void GetIdentifierList()
        {
            var text = Document.Text;

            await Task.Factory.StartNew(new Action(async () =>
            {
                var identifiers = await RoslynHost.GetIdentifiers(text);

                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Identifiers = new ObservableCollection<Identifier>(identifiers);
                }));
            }));
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
