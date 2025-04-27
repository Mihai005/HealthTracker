namespace HealthTrackerMVVM.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using MyNavService = HealthTrackerMVVM.Service.NavigationService;

    public sealed partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            MyNavService.Instance.NavigateTo(new LogInPage(), this);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MyNavService.Instance.NavigateTo(new SignUpPage(), this);
        }
    }
}
