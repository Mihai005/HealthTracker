namespace HealthTrackerMVVM.ViewModels
{
    using System.ComponentModel;
    using System.Configuration;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using HealthTrackerMVVM.Models;
    using HealthTrackerMVVM.Utils;

    public class SignUpViewModel : INotifyPropertyChanged
    {
        private readonly SignUpModel model;

        public event EventHandler<string>? ShowMessageEvent;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SignUpViewModel()
        {
            this.model = new SignUpModel();
        }

        public void ShowMessage(string message)
        {
            this.ShowMessageEvent?.Invoke(this, message);
        }

        public async Task<bool> SignUpAsync(string username, string password, string repeatPassword)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var url = $"{baseUrl}/api/auth/signup";

            var requestBody = new
            {
                username,
                password,
                repeatPassword,
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await ApiClient.Client.PostAsync(url, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    this.ShowMessage("Signin Successful");
                    this.model.Username = username;
                    this.model.Password = password;
                    this.model.RepeatPassword = repeatPassword;
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
