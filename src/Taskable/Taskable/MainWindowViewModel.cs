using System;

namespace TaskableApp
{
    public class MainWindowViewModel : BindableBase
    {
        
        public GenericCommand SettingsDialogCommand;

        public MainWindowViewModel()
        {            
            SettingsDialogCommand = new GenericCommand((Action) ShowSettingsDialog);
        }

        public void ShowSettingsDialog()
        {
            
        }
    }
}
