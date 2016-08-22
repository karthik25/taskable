using System.Windows;
using System.Windows.Controls;
using TaskableApp.ViewModels;

namespace TaskableApp.Views
{
    /// <summary>
    /// Interaction logic for CodeEditorView.xaml
    /// </summary>
    public partial class CodeEditorView : UserControl
    {
        public CodeEditorView()
        {
            InitializeComponent();
        }

        private void textEditor_Loaded(object sender, RoutedEventArgs e)
        {
            textEditor.Focus();
        }

        private void IdList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var model = (CodeEditorViewModel)this.DataContext;
            textEditor.ScrollToLine(model.SelectedIdentifier.Line);
            textEditor.CaretOffset = model.SelectedIdentifier.Offset;
            textEditor.Focus();
        }
    }
}
