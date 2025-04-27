namespace HealthTrackerMVVM.Models
{
    public class SignUpModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public SignUpModel()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.RepeatPassword = string.Empty;
        }
    }
}
