﻿using System.IO;
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
            this.DataContext = new CodeCollectionViewModel();
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
    }
}
