using System;
using System.Collections.Generic;
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
using Monitor.Settings;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for RunToAddress.xaml
    /// </summary>
    public partial class RunToAddressWindow : Window
    {
        private SettingsContainer _settings;

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
