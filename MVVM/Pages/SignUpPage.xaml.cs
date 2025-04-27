namespace HealthTrackerMVVM.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using HealthTrackerMVVM.ViewModels;
    using MyNavService = HealthTrackerMVVM.Service.NavigationService;

    public sealed partial class SignUpPage: Page
    {
        private readonly SignUpViewModel viewModel;

        public SignUpPage()
        {
            this.InitializeComponent();
            this.viewModel = new SignUpViewModel();
            this.DataContext = this.viewModel;
            this.viewModel.ShowMessageEvent += this.ViewModel_ShowMessageEvent;
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            string username = this.usernameBox.Text;
            string password = this.passwordBox.Password;
            string confirmPassword = this.confirmPasswordBox.Password;
            bool success = await this.viewModel.SignUpAsync(username, password, confirmPassword);

            if (success)
            {
                MyNavService.Instance.NavigateTo(new GoalsPage(username), this);
            }
        }

        private void GoBack_Clicked(object sender, EventArgs e)
        {
            MyNavService.Instance.GoBack();
        }

        private void ViewModel_ShowMessageEvent(object sender, string message) => MessageBox.Show(message);
    }
}
