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
            textEditor.ScrollToLine(model.SelectedIdentifier.StartLine);
            textEditor.CaretOffset = model.SelectedIdentifier.OffsetStart;
            textEditor.Focus();
        }

        private void IdCombo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            IdCombo.IsDropDownOpen = true;
        }

        private void IdCombo_LostFocus(object sender, RoutedEventArgs e)
        {
            var model = (CodeEditorViewModel)this.DataContext;
            if (model != null && model.SelectedIdentifier != null)
            {
                textEditor.ScrollToLine(model.SelectedIdentifier.StartLine);
                textEditor.CaretOffset = model.SelectedIdentifier.OffsetStart;
                IdCombo.SelectedItem = null;
                model.HideNav();
                textEditor.Focus();
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var model = (CodeEditorViewModel)this.DataContext;
            if (model != null)
            {
                model.CaretPositionChanged += Model_CaretPositionChanged;
            }
        }

        private void Model_CaretPositionChanged(object sender, System.EventArgs e)
        {
            var model = (CodeEditorViewModel)this.DataContext;
            textEditor.ScrollToLine(model.NextIdentifier.StartLine);
            textEditor.CaretOffset = model.NextIdentifier.OffsetStart;
            textEditor.Focus();
        }
    }
}
