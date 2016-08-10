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

        private void BtnAddParam_Click(object sender, RoutedEventArgs e)
        {
            var selectorModel = (TaskSelectorViewModel)this.DataContext;
            var paramView = new AddParameterView(selectorModel.ParameterViewModel);
            paramView.ShowDialog();
        }
    }
}
