using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Monitor.Settings;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for RunAndConnectWindow.xaml
    /// </summary>
    public partial class RunAndConnectWindow : Window
    {
        private SettingsContainer _settings;

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
