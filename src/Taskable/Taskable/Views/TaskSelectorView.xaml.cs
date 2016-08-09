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
            this.DataContext = new TaskSelectorViewModel();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddParam_Click(object sender, RoutedEventArgs e)
        {
            var paramView = new AddParameterView();
            paramView.ShowDialog();
        }
    }
}
