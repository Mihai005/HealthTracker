namespace HealthTrackerMVVM
{
    using System.Windows;
    using HealthTrackerMVVM.Pages;
    using HealthTrackerMVVM.Service;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            NavigationService.Instance.Initialize(this.MainFrame);
            NavigationService.Instance.NavigateTo(new AuthenticationPage(), this);
        }
    }
}