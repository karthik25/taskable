using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskableApp
{
    public class GenericCommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<Task> _AsyncTaskExecuteMethod;

        public event EventHandler CanExecuteChanged;

        public GenericCommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public GenericCommand(Func<Task> asyncAction)
        {
            _AsyncTaskExecuteMethod = asyncAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (_AsyncTaskExecuteMethod != null)
            {
                await _AsyncTaskExecuteMethod().ConfigureAwait(false);
            }

            if (_TargetExecuteMethod != null)
                _TargetExecuteMethod();
        }
    }

    public class GenericCommand<T> : ICommand
    {
        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;

        public GenericCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public GenericCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        
        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }
    }
}
