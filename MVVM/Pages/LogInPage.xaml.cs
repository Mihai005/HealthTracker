namespace HealthTrackerMVVM.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using HealthTrackerMVVM.ViewModels;
    using MyNavService = HealthTrackerMVVM.Service.NavigationService;

    public partial class LogInPage : Page
    {
        private readonly LogInViewModel viewModel;

        public LogInPage()
        {
            this.InitializeComponent();
            this.viewModel = new LogInViewModel();
            this.DataContext = this.viewModel;
            this.viewModel.ShowMessageEvent += this.ViewModel_ShowMessageEvent;
        }

        private async void LogIn_Clicked(object sender, RoutedEventArgs e)
        {
            string username = this.usernameBox.Text;
            string password = this.passwordBox.Password;

            bool success = await this.viewModel.LogInAsync(username, password);

            if (success)
            {
                MyNavService.Instance.NavigateTo(new DashboardPage(username), this);
            }
        }

        private void GoBack_Clicked(object sender, RoutedEventArgs e)
        {
            MyNavService.Instance?.GoBack();
        }

        private void ViewModel_ShowMessageEvent(object sender, string message) => MessageBox.Show(message);
    }
}
