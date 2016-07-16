using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TaskableApp.ViewModels
{
    public class CodeEditorViewModel : BindableBase
    {
        public GenericCommand SaveDocumentCommand
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

        public CodeEditorViewModel()
        {
            Title = "New Document";
            Document = new TextDocument();
            SaveDocumentCommand = new GenericCommand(Save);
        }

        public CodeEditorViewModel(string filePath)
        {
            if (File.Exists(filePath))
            {
                CurrentFile = filePath;
                Title = Path.GetFileName(filePath);
                Document = FileLoader.LoadFile(filePath);
            }
            SaveDocumentCommand = new GenericCommand(Save);
        }
        
        private async Task Save()
        {
            if (string.IsNullOrEmpty(CurrentFile))
                return;

            var contents = Document.Text;
            using (var writer = new StreamWriter(CurrentFile, false))
            {
                await writer.WriteAsync(contents);
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
