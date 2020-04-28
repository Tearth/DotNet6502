using System.Windows;
using Monitor.Settings;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        private SettingsContainer _settings;

        public ConnectWindow(SettingsContainer settings)
        {
            InitializeComponent();
            _settings = settings;

            DataContext = _settings.Data;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.Save();

            DialogResult = true;
            Close();
        }
    }
}
