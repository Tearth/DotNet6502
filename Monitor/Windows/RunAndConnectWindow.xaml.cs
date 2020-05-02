using System.Windows;
using Microsoft.Win32;
using Monitor.Settings;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for RunAndConnectWindow.xaml
    /// </summary>
    public partial class RunAndConnectWindow : Window
    {
        private readonly SettingsContainer _settings;

        public RunAndConnectWindow(SettingsContainer settings)
        {
            InitializeComponent();
            _settings = settings;

            DataContext = _settings.Data;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _settings.Data.Path = openFileDialog.FileName;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.Save();

            DialogResult = true;
            Close();
        }
    }
}
