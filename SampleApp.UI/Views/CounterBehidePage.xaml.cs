using System.Windows.Controls;

namespace SampleApp.UI.Views
{
    public partial class CounterBehidePage : Page
    {
        private int _count;

        public CounterBehidePage()
        {
            InitializeComponent();
            UpdateView();
        }

        private void IncrementButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _count++;
            UpdateView();
        }

        private void DecrementButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _count--;
            UpdateView();
        }

        private void UpdateView()
        {
            CountText.Text = _count.ToString(System.Globalization.CultureInfo.InvariantCulture);
            DecrementButton.IsEnabled = _count > 0;
        }
    }
}
