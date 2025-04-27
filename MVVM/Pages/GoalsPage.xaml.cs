namespace HealthTrackerMVVM.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using HealthTrackerMVVM.ViewModels;
    using MyNavService = HealthTrackerMVVM.Service.NavigationService;

    public partial class GoalsPage : Page
    {
        private readonly GoalsViewModel viewmodel;
        private readonly string username;

        public GoalsPage(string username)
        {
            this.InitializeComponent();
            this.viewmodel = new GoalsViewModel(username);
            this.DataContext = this.viewmodel;
            this.username = username;
            this.viewmodel.ShowMessageEvent += this.ViewModel_ShowMessageEvent;
        }

        public async void SaveGoals_Click(object sender, RoutedEventArgs e)
        {
            string calorieGoal = this.CalorieGoalBox.Text;
            string waterGoal = this.WaterGoalBox.Text;
            string stepsGoal = this.StepsGoalBox.Text;

            bool result = await this.viewmodel.AddGoals(calorieGoal, waterGoal, stepsGoal);

            if (result)
            {
                MyNavService.Instance.NavigateTo(new DashboardPage(this.username), this);
            }
        }

        private void ViewModel_ShowMessageEvent(object sender, string message) => MessageBox.Show(message);
    }
}
