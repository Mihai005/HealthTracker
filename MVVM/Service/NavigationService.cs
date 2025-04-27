namespace HealthTrackerMVVM.Service
{
    using System.Windows.Controls;
    using HealthTrackerMVVM.ViewModels;

    public class NavigationService
    {
        private static NavigationService? instance;
        private Frame? mainFrame;

        public static NavigationService Instance => instance ??= new NavigationService();

        public void Initialize(Frame mainFrame)
        {
            this.mainFrame = mainFrame;
        }

        public void NavigateTo(Page page, object? parameter = null)
        {
            this.mainFrame?.Navigate(page, parameter);
        }

        public void GoBack()
        {
            if (this.mainFrame?.CanGoBack == true)
            {
                this.mainFrame.GoBack();
            }
        }

        internal void NavigateTo(Type type, LogInViewModel logInViewModel)
        {
            this.mainFrame?.Navigate(type, logInViewModel);
        }
    }
}
