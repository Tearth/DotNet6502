using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Monitor.Debugger;
using Monitor.Settings;
using Monitor.ViewModels;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = new MainWindowViewModel();
        private SettingsContainer _settings = new SettingsContainer("settings.json");
        private DebuggerClient _debugger = new DebuggerClient();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void GoToAddressButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Stack +=
@"{\rtf1\ansi\deff0
{\fonttbl {\f0 Consolas;}}
{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}
\fs18
\cf2 0x12: \cf1 0x23 <- stack pointer\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
}";

            _viewModel.DisassembledCode +=
                @"{\rtf1\ansi\deff0
{\fonttbl {\f0 Consolas;}}
{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}
\fs18
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
}";

            _viewModel.Memory +=
                @"{\rtf1\ansi\deff0
{\fonttbl {\f0 Consolas;}}
{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}
\fs18
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
}";

            _viewModel.Bus +=
                @"{\rtf1\ansi\deff0
{\fonttbl {\f0 Consolas;}}
{\colortbl;\red255\green255\blue255;\red150\green150\blue150;}
\fs18
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
\cf2 0x12: \cf1 0x23\line
}";
        }

        private async void ConnectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new ConnectWindow(_settings);
            if (window.ShowDialog() == true)
            {
                StatusLabel.Text = $"Status: connecting to {_settings.Data.Address}...";
                var result = await _debugger.Connect(_settings.Data.Address);
                if (!result.Success)
                {
                    StatusLabel.Text = "Status: connection error";
                    MessageBox.Show(result.ErrorMessage, "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void RunAndConnectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new RunAndConnectWindow(_settings);
            if (window.ShowDialog() == true)
            {
                StatusLabel.Text = $"Status: running {_settings.Data.Path}...";

                var runEmulatorResult = RunEmulator();
                if (!runEmulatorResult.Success)
                {
                    StatusLabel.Text = "Status: connection error";
                    MessageBox.Show(runEmulatorResult.ErrorMessage, "Emulator error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await Task.Delay(500);
                StatusLabel.Text = $"Status: connecting to {_settings.Data.Address}...";

                var result = await _debugger.Connect(_settings.Data.Address);
                if (!result.Success)
                {
                    StatusLabel.Text = "Status: connection error";
                    MessageBox.Show(result.ErrorMessage, "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                StatusLabel.Text = $"Status: connected to {_settings.Data.Address}";
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private (bool Success, string ErrorMessage) RunEmulator()
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"{_settings.Data.Path} {_settings.Data.Arguments}",
                    WorkingDirectory = Path.GetDirectoryName(_settings.Data.Path)
                };

                Process.Start(processStartInfo);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, null);
        }
    }
}
