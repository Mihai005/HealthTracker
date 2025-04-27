namespace HealthTrackerMVVM.ViewModels
{
    using System.Configuration;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using HealthTrackerMVVM.Utils;

    public class GoalsViewModel
    {
        private readonly string username;

        public event EventHandler<string>? ShowMessageEvent;

        public GoalsViewModel(string username)
        {
            this.username = username;
        }

        public void ShowMessage(string message)
        {
            this.ShowMessageEvent?.Invoke(this, message);
        }

        public async Task<bool> AddGoals(string calorieGoal, string waterGoal, string stepsGoal)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/goals/add";
            var requestBody = new
            {
                this.username,
                calorieGoal,
                waterGoal,
                stepsGoal,
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return true;
                }
                else
                {
                    this.ShowMessage(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
                return false;
            }
        }
    }
}
