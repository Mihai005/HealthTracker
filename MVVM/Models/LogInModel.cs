namespace HealthTrackerMVVM.Models
{
    public class LogInModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public LogInModel()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
        }
    }
}
