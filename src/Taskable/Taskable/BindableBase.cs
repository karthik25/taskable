using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using TaskableCore;

namespace TaskableApp
{
    public class BindableBase : INotifyPropertyChanged
    {
        protected Options _options;

        public BindableBase()
        {
            _options = ParseUserSpecificOptions();
        }

        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private Options ParseUserSpecificOptions()
        {
            var baseConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = string.Format(@"{0}\taskable.yml", baseConfigPath);
            if (!File.Exists(configPath))
            {
                Options.CreateDefaultOptionsFile(configPath);
            }
            return Options.ParseFromFile(configPath);
        }

        protected Options UpdateUserSpecificOptions(Options options)
        {
            var baseConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = string.Format(@"{0}\taskable.yml", baseConfigPath);
            var updatedOptions = Options.WriteToFile(options, configPath);
            this._options = updatedOptions;
            return updatedOptions;
        }
    }
}
