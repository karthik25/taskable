using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskableApp.ViewModels;

namespace TaskableApp.Views
{
    /// <summary>
    /// Interaction logic for AddParameterView.xaml
    /// </summary>
    public partial class AddParameterView : Window
    {
        public AddParameterView()
        {
            InitializeComponent();
        }

        public AddParameterView(AddParameterViewModel model) : this()
        {
            this.DataContext = model;
            model.Save += Model_Save;
        }

        private void Model_Save(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            TxtParam.Focus();
        }
    }
}
