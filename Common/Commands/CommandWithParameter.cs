using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Common.Commands
{
    public class CommandWithParameter<T> : ICommand
    {

        private readonly Action<T> action;


        public CommandWithParameter(Action<T> action)
        {
            this.action = action;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action.Invoke((T)parameter);
        }
    }
}
