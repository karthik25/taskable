using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskableApp.Abstract;
using TaskableApp.ViewModels;

namespace TaskableApp.Views
{
    /// <summary>
    /// Interaction logic for CodeCollectionView.xaml
    /// </summary>
    public partial class CodeCollectionView : UserControl
    {
        public CodeCollectionView()
        {
            InitializeComponent();
        }

        private void trvFiles_Loaded(object sender, RoutedEventArgs e)
        {
            var item = trvFiles.Items[0];
            TreeViewItem treeItem = this.trvFiles.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
            if (treeItem != null)
            {
                treeItem.IsExpanded = true;
            }
        }

        private void TreeViewItem_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item != null && item.DataContext != null)
            {
                var fileItem = item.DataContext as INamedFileEntry;
                if (File.Exists(fileItem.Path))
                {
                    ((CodeCollectionViewModel)this.DataContext).AddDocument(fileItem.Path);
                }
            }
        }

        private void DockingManager_DocumentClosing(object sender, Xceed.Wpf.AvalonDock.DocumentClosingEventArgs e)
        {
            e.Cancel = true;            
            var item = (CodeEditorViewModel)e.Document.Content;
            ((CodeCollectionViewModel)this.DataContext).CloseDocument(item);
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ((CodeCollectionViewModel)this.DataContext).AddDocument(openFileDialog.FileName);
            }
        }

        private async void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var model = (CodeCollectionViewModel)this.DataContext;
            await model.RepopulateTree();
        }
    }
}
