namespace HealthTrackerMVVM.Models
{
    using System.Text.Json.Serialization;

    public class DailyLogModel
    {
        [JsonPropertyName("logDate")]
        public DateTime Date { get; set; }

        public List<MealModel> Meals { get; set; } = new ();

        [JsonPropertyName("waterIntake")]
        public int WaterConsumed { get; set; }

        [JsonPropertyName("stepsTaken")]
        public int StepsTaken { get; set; }
    }
}
