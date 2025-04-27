namespace HealthTrackerMVVM.Models
{
    using System.Text.Json.Serialization;

    public class UserGoalsModel
    {
        [JsonPropertyName("calorieGoal")]
        public int DailyCalorieGoal { get; set; }

        [JsonPropertyName("waterGoal")]
        public int WaterGoalMl { get; set; }

        [JsonPropertyName("stepsGoal")]
        public int StepsGoal { get; set; }

        public UserGoalsModel(int calories, int waterMl, int steps)
        {
            this.DailyCalorieGoal = calories;
            this.WaterGoalMl = waterMl;
            this.StepsGoal = steps;
        }

        public UserGoalsModel()
        {

        }
    }
}
