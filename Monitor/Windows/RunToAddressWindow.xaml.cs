using System.Windows;
using Monitor.Settings;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for RunToAddress.xaml
    /// </summary>
    public partial class RunToAddressWindow : Window
    {
        private readonly SettingsContainer _settings;

        public RunToAddressWindow(SettingsContainer settings)
        {
            InitializeComponent();
            _settings = settings;

            DataContext = _settings.Data;
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.Save();

            DialogResult = true;
            Close();
        }
    }
}
