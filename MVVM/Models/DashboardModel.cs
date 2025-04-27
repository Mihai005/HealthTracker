namespace HealthTrackerMVVM.Models
{
    using System.Collections.ObjectModel;

    public class DashboardModel
    {
        public string Username { get; set; }

        public DailyLogModel TodayLog { get; set; }

        public ObservableCollection<MealModel> MealsToday { get; set; }

        public UserGoalsModel Goals { get; set; }

        public string WaterProgressText => $"{this.TodayLog.WaterConsumed} ml / {this.Goals.WaterGoalMl} ml";

        public double WaterPercentage => this.Goals.WaterGoalMl == 0 ? 0 : (double)this.TodayLog.WaterConsumed / this.Goals.WaterGoalMl * 100;

        public double StepsPercentage => this.Goals.StepsGoal == 0 ? 0 : (double)this.TodayLog.StepsTaken / this.Goals.StepsGoal * 100;

        public double CaloriePercentage => this.Goals.DailyCalorieGoal == 0 ? 0 : (double)this.TotalCalories / this.Goals.DailyCalorieGoal * 100;

        public string AiResponse { get; set; }

        public string ChatInputText { get; set; }

        public int TotalCalories => this.MealsToday.Sum(m => m.Calories);

        public bool IsLoadingAiResponse;

        public DashboardModel(string username, UserGoalsModel goals, DailyLogModel log, List<MealModel> meals)
        {
            this.Username = username;
            this.TodayLog = log;
            this.MealsToday = new ObservableCollection<MealModel>(meals);
            this.Goals = goals;
            this.AiResponse = string.Empty;
            this.ChatInputText = string.Empty;
        }

        public DashboardModel()
        {

        }
    }
}
