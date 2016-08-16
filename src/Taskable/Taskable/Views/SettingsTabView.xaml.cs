using System.Windows;
using System.Windows.Controls;
using TaskableApp.ViewModels;

namespace TaskableApp.Views
{
    /// <summary>
    /// Interaction logic for SettingsTabView.xaml
    /// </summary>
    public partial class SettingsTabView : UserControl
    {
        public SettingsTabView()
        {
            InitializeComponent();
        }

        public SettingsTabView(TaskSelectorViewModel model) : this()
        {
            this.DataContext = model;
        }

        private void BtnAddFolder_Click(object sender, RoutedEventArgs e)
        {
            var selectorModel = (SettingsTabViewModel)this.DataContext;
            var fileView = new FileOrFolderSelectorView(selectorModel.FolderSelectionViewModel);
            fileView.ShowDialog();
        }

        private void BtnAddFile_Click(object sender, RoutedEventArgs e)
        {
            var selectorModel = (SettingsTabViewModel)this.DataContext;
            var folderView = new FileOrFolderSelectorView(selectorModel.FileSelectionViewModel);
            folderView.ShowDialog();
        }
    }
}
