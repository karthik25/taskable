using System;

namespace TaskableApp.ViewModels
{
    public class AddParameterViewModel
    {
        public string Parameter { get; set; }

        public event EventHandler Save;

        public GenericCommand<object> SaveCommand { get; set; }

        public AddParameterViewModel()
        {
            SaveCommand = new GenericCommand<object>(x => this.Save(this, new EventArgs()));
        }

        public void Reset()
        {
            
        }
    }
}
