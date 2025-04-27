namespace HealthTrackerMVVM.Pages
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class AddMealWindow : Window
    {
        public AddMealWindow()
        {
            this.InitializeComponent();
        }

        public string SelectedMealType { get; private set; } = string.Empty;

        public int Calories { get; private set; }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(this.CaloriesInput.Text, out int cal))
            {
                this.SelectedMealType = (this.MealTypeCombo.SelectedItem as ComboBoxItem)?.Content.ToString() ?? string.Empty;
                this.Calories = cal;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number for calories.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
