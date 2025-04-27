namespace HealthTrackerMVVM.ViewModels
{
    using System.ComponentModel;
    using System.Configuration;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Windows;
    using System.Windows.Input;
    using HealthTrackerMVVM.Models;
    using HealthTrackerMVVM.Pages;
    using HealthTrackerMVVM.Utils;

    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<string>? ShowMessageEvent;

        public DashboardModel Model { get; set; }

        public ICommand AddMealCommand { get; set; }

        public ICommand AddWaterCommand { get; set; }

        public ICommand AddStepsCommand { get; set; }

        public ICommand AskAiCommand { get; set; }

        public ICommand UpdateGoalsCommand { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public DashboardViewModel(string username)
        {
            this.AddMealCommand = new RelayCommand(AddMeal);
            this.AddWaterCommand = new RelayCommand(AddWater);
            this.AddStepsCommand = new RelayCommand(AddSteps);
            this.AskAiCommand = new RelayCommand(AskAi);
            this.UpdateGoalsCommand = new RelayCommand(UpdateGoals);
            Task<UserGoalsModel> goals = this.LoadGoalsFromDatabase(username);
            Task<List<MealModel>> meals = this.LoadDailyMealsFromDatabase(username);
            Task<DailyLogModel> dailyLog = this.LoadDailyLogsFromDatabase(username);
            this.Model = new DashboardModel(username, goals.Result, dailyLog.Result, meals.Result);
        }

        public void ShowMessage(string message)
        {
            this.ShowMessageEvent?.Invoke(this, message);
        }

        private async Task<DailyLogModel> LoadDailyLogsFromDatabase(string username)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/daily/get/{Uri.EscapeDataString(username)}";

            try
            {
                HttpResponseMessage response = await ApiClient.Client.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var log = JsonSerializer.Deserialize<DailyLogModel>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    });
                    return log ?? new DailyLogModel();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    this.ShowMessage($"Error from API: {errorMessage}");
                    return new DailyLogModel();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage($"Exception: {ex.Message}");
                return new DailyLogModel();
            }
        }

        private async Task<List<MealModel>> LoadDailyMealsFromDatabase(string username)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/daily/meals/get/{Uri.EscapeDataString(username)}";

            try
            {
                HttpResponseMessage response = await ApiClient.Client.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var log = JsonSerializer.Deserialize<List<MealModel>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    });
                    return log ?? new List<MealModel>();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    this.ShowMessage($"Error from API: {errorMessage}");
                    return new List<MealModel>();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage($"Exception: {ex.Message}");
                return new List<MealModel>();
            }
        }

        private async void AddMeal()
        {
            var dialog = new AddMealWindow();
            if (dialog.ShowDialog() == true)
            {
                if (Enum.TryParse(dialog.SelectedMealType, out MealType mealType))
                {
                    MealModel meal = new MealModel
                    {
                        Type = mealType,
                        Calories = dialog.Calories,
                    };

                    await this.AddMealToDb(this.Model.Username, meal).ConfigureAwait(false);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Model.MealsToday.Add(meal);
                        this.OnPropertyChanged(nameof(this.Model));
                    });

                    if (this.Model.TotalCalories >= this.Model.Goals.DailyCalorieGoal)
                    {
                        this.ShowMessage("🎉 Calories goal reached!");
                    }
                }
            }
        }

        private async void AddSteps()
        {
            var window = new AddStepsWindow();
            if (window.ShowDialog() == true)
            {
                await this.AddStepsToDb(this.Model.Username, window.steps).ConfigureAwait(false);
                this.Model.TodayLog.StepsTaken += window.steps;
                this.OnPropertyChanged(nameof(this.Model));

                if (this.Model.TodayLog.StepsTaken >= this.Model.Goals.StepsGoal)
                {
                    this.ShowMessage("🎉 Steps goal reached!");
                }
            }
        }

        private async void AskAi()
        {
            this.Model.IsLoadingAiResponse = true;
            this.OnPropertyChanged(nameof(this.Model));
            this.Model.AiResponse = await this.AskAiFromBackend(this.Model.ChatInputText).ConfigureAwait(false);
            this.Model.IsLoadingAiResponse = false;
            this.OnPropertyChanged(nameof(this.Model));
        }

        private async Task<string> AskAiFromBackend(string prompt)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/ai/ask";

            var requestBody = new
            {
                prompt,
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return (await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Trim();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    this.ShowMessage($"Error from API: {errorMessage}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
                return string.Empty;
            }
        }

        private async void UpdateGoals()
        {
            var window = new UpdateGoalsWindow(this.Model.Goals.DailyCalorieGoal, this.Model.Goals.WaterGoalMl, this.Model.Goals.StepsGoal);
            if (window.ShowDialog() == true)
            {
                await this.UpdateGoalsToDb(this.Model.Username, window.caloriesGoal, window.waterGoal, window.stepsGoal);
                this.OnPropertyChanged(nameof(this.Model));
            }
        }

        private async Task UpdateGoalsToDb(string username, int calories, int water, int steps)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/goals/update";

            var requestBody = new
            {
                username,
                calorieGoal = calories,
                waterGoal = water,
                stepsGoal = steps,
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    this.Model.Goals.DailyCalorieGoal = calories;
                    this.Model.Goals.WaterGoalMl = water;
                    this.Model.Goals.StepsGoal = steps;
                    this.ShowMessage("Goals updated successfully!");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        private async Task AddStepsToDb(string username, int steps)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/daily/steps/add";

            var requestBody = new
            {
                username,
                steps = steps.ToString(),
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        private async Task AddMealToDb(string username, MealModel meal)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/daily/meals/add";

            var requestBody = new
            {
                username,
                type = meal.Type.ToString(),
                calories = meal.Calories.ToString(),
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        private async void AddWater()
        {
            const int glassSize = 250;
            await this.AddWaterToDb(this.Model.Username, glassSize);
            this.Model.TodayLog.WaterConsumed += glassSize;
            this.OnPropertyChanged(nameof(this.Model));

            if (this.Model.TodayLog.WaterConsumed >= this.Model.Goals.WaterGoalMl)
            {
                MessageBox.Show("🎉 Water goal reached!", "Success");
            }
        }

        private async Task AddWaterToDb(string username, int water)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/daily/water/add";

            var requestBody = new
            {
                username,
                quantity = water.ToString(),
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        private async Task<UserGoalsModel> LoadGoalsFromDatabase(string username)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/goals/get/{Uri.EscapeDataString(username)}";

            try
            {
                HttpResponseMessage response = await ApiClient.Client.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var goal = JsonSerializer.Deserialize<UserGoalsModel>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    });
                    return goal ?? new UserGoalsModel(0, 0, 0);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    this.ShowMessage($"Error from API: {errorMessage}");
                    return new UserGoalsModel(0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage($"Exception: {ex.Message}");
                return new UserGoalsModel(0, 0, 0);
            }
        }
    }
}
