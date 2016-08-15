using System.Windows;
using System.Windows.Controls;
using TaskableApp.ViewModels;

namespace TaskableApp.Views
{
    /// <summary>
    /// Interaction logic for TaskSelectorView.xaml
    /// </summary>
    public partial class TaskSelectorView : UserControl
    {
        public TaskSelectorView()
        {
            InitializeComponent();
        }

        private void BtnAddParam_Click(object sender, RoutedEventArgs e)
        {
            var selectorModel = (TaskSelectorViewModel)this.DataContext;
            var paramView = new AddParameterView(selectorModel.ParameterViewModel);
            paramView.ShowDialog();
        }

        private void BtnAddFolder_Click(object sender, RoutedEventArgs e)
        {
            var selectorModel = (TaskSelectorViewModel)this.DataContext;
            var fileView = new FileOrFolderSelectorView(selectorModel.FileSelectionViewModel);
            fileView.ShowDialog();
        }

        private void BtnAddFile_Click(object sender, RoutedEventArgs e)
        {
            var selectorModel = (TaskSelectorViewModel)this.DataContext;
            var folderView = new FileOrFolderSelectorView(selectorModel.FolderSelectionViewModel);
            folderView.ShowDialog();
        }
    }
}
