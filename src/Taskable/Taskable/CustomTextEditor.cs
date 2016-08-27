using ICSharpCode.AvalonEdit;
using System.Windows;
using System.Windows.Input;

namespace TaskableApp
{
    public class CustomTextEditor : TextEditor
    {
        #region Caret Offset.
        /// <summary>
        /// DependencyProperty for the TextEditorCaretOffset binding. 
        /// </summary>
        public static DependencyProperty CaretOffsetProperty =
            DependencyProperty.Register("CaretOffset", typeof(int), typeof(CustomTextEditor),
            new PropertyMetadata((obj, args) =>
            {
                CustomTextEditor target = (CustomTextEditor)obj;
                if (target.CaretOffset != (int)args.NewValue)
                    target.CaretOffset = (int)args.NewValue;
            }));

        /// <summary>
        /// Access to the SelectionStart property.
        /// </summary>
        public new int CaretOffset
        {
            get { return base.CaretOffset; }
            set { SetValue(CaretOffsetProperty, value); }
        }
        #endregion // Caret Offset.

        public CustomTextEditor()
        {
            Options = new TextEditorOptions
            {
                ConvertTabsToSpaces = true,
                AllowScrollBelowDocument = true
            };
            TextArea.TextEntering += TextArea_TextEntering;
            TextArea.TextEntered += TextArea_TextEntered;
            TextArea.Caret.PositionChanged += Caret_PositionChanged;
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

        private void Caret_PositionChanged(object sender, System.EventArgs e)
        {
            this.CaretOffset = TextArea.Caret.Offset;
        }
    }
}
