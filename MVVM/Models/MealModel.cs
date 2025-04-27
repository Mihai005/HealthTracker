namespace HealthTrackerMVVM.Models
{
    using System.Text.Json.Serialization;

    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack,
    }

    public class MealModel
    {
        [JsonPropertyName("mealType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MealType Type { get; set; }

        public int Calories { get; set; }

        public string DisplayName => $"{this.Type} - {this.Calories} kcal";
    }
}
