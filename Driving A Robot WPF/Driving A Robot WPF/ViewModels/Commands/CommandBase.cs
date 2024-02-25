using System.Windows.Input;

namespace Driving_A_Robot_WPF.ViewModels.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);
        
        protected void OnCanExecuteChanged() 
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
