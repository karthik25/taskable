using ICSharpCode.AvalonEdit;
using System.Windows.Input;

namespace TaskableApp
{
    public class CustomTextEditor : TextEditor
    {
        public CustomTextEditor()
        {
            Options = new TextEditorOptions
            {
                ConvertTabsToSpaces = true,
                AllowScrollBelowDocument = true
            };
            TextArea.TextEntering += TextArea_TextEntering;
            TextArea.TextEntered += TextArea_TextEntered;
            ShowLineNumbers = true;
        }

        public void OpenFile(string fileName)
        {
            Load(fileName);
            Document.FileName = fileName;
        }

        public void SaveFile()
        {
            if (!string.IsNullOrEmpty(Document.FileName))
            {
                Save(Document.FileName);
            }
        }

        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
