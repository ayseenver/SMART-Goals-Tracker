using Goals.DataModel;
using System;
using System.Windows.Input;

namespace Goals.Commands
{
    class DeleteButtonClick : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            App.DataModel.RemoveGoal((Goal)parameter);
        }
    }
}
