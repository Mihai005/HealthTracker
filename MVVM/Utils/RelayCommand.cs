namespace HealthTrackerMVVM.Utils
{
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Action<object> executeWithParam;
        private readonly Action execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand(Action<object> executeWithParam, Predicate<object> canExecute = null)
        {
            this.executeWithParam = executeWithParam ?? throw new ArgumentNullException(nameof(executeWithParam));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => this.canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter)
        {
            if (this.execute != null)
            {
                this.execute();
            }
            else if (this.executeWithParam != null)
            {
                this.executeWithParam(parameter);
            }
        }
    }

}