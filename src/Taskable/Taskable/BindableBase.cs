using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TaskableCore;

namespace TaskableApp
{
    public class BindableBase : INotifyPropertyChanged
    {
        protected Options _options;

        public BindableBase()
        {
            ParseUserSpecificOptions();
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

        protected void ParseUserSpecificOptions()
        {
            var baseConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = string.Format(@"{0}\taskable.yml", baseConfigPath);
            if (!File.Exists(configPath))
            {
                Options.CreateDefaultOptionsFile(configPath);
            }
            this._options = Options.ParseFromFile(configPath);
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

    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uie.Focus(); // Don't care about false values.
            }
        }
    }
}
