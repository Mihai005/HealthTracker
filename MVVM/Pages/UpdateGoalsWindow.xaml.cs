namespace HealthTrackerMVVM.Pages
{

    using System.Windows;

    public partial class UpdateGoalsWindow : Window
    {
        public int caloriesGoal { get; private set; }

        public int waterGoal { get; private set; }

        public int stepsGoal { get; private set; }

        public UpdateGoalsWindow(int currentCalories, int currentWater, int currentSteps)
        {
            this.InitializeComponent();
            this.CalorieGoalBox.Text = currentCalories.ToString();
            this.WaterGoalBox.Text = currentWater.ToString();
            this.StepsGoalBox.Text = currentSteps.ToString();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            string caloriesGoal = this.CalorieGoalBox.Text;
            string waterGoal = this.WaterGoalBox.Text;
            string stepsGoal = this.StepsGoalBox.Text;

            if (int.TryParse(caloriesGoal, out int caloriesInt) &&
                int.TryParse(waterGoal, out int waterInt) &&
                int.TryParse(stepsGoal, out int stepsInt))
            {
                this.caloriesGoal = caloriesInt;
                this.waterGoal = waterInt;
                this.stepsGoal = stepsInt;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid input (numbers).");
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
