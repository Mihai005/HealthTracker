namespace HealthTrackerMVVM.ViewModels
{
    using System.ComponentModel;
    using System.Configuration;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using HealthTrackerMVVM.Models;
    using HealthTrackerMVVM.Utils;

    public class LogInViewModel : INotifyPropertyChanged
    {
        private readonly LogInModel model;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<string>? ShowMessageEvent;


        public LogInViewModel()
        {
            this.model = new LogInModel();
        }

        public void ShowMessage(string message)
        {
            this.ShowMessageEvent?.Invoke(this, message);
        }

        public async Task<bool> LogInAsync(string username, string password)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/auth/login";

            var requestBody = new
            {
                username,
                password,
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    this.ShowMessage("Login Successful");
                    this.model.Username = username;
                    this.model.Password = password;
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
