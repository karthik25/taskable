﻿using System.Windows;
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

        private void BtnGoto_Click(object sender, RoutedEventArgs e)
        {
            var model = (CodeEditorViewModel)this.DataContext;
            textEditor.CaretOffset = model.SelectedIdentifier.Offset;
        }
    }
}
