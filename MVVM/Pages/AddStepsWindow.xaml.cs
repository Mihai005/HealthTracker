namespace HealthTrackerMVVM.Pages
{
    using System.Windows;

    public partial class AddStepsWindow : Window
    {
        public int steps { get; private set; }

        public AddStepsWindow()
        {
            this.InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string stepsAmount = this.StepsInput.Text;
            if (int.TryParse(stepsAmount, out int stepsInt))
            {
                this.steps = stepsInt;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number for steps.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
