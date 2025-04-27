namespace HealthTrackerMVVM.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using HealthTrackerMVVM.ViewModels;
    using MyNavService = HealthTrackerMVVM.Service.NavigationService;

    public partial class DashboardPage : Page
    {
        private DashboardViewModel viewModel;

        public DashboardPage(string username)
        {
            this.InitializeComponent();
            this.viewModel = new DashboardViewModel(username);
            this.DataContext = this.viewModel;
            this.viewModel.ShowMessageEvent += this.ViewModel_ShowMessageEvent;
        }

        private void ViewModel_ShowMessageEvent(object sender, string message) => MessageBox.Show(message);

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MyNavService.Instance.NavigateTo(new AuthenticationPage(), this);
        }
    }
}
