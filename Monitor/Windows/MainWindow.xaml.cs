using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private readonly MainWindowViewModel _viewModel;
        private readonly SettingsContainer _settings;
        private readonly DebuggerClient _debugger;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel();
            _settings = new SettingsContainer("settings.json");
            _debugger = new DebuggerClient(_viewModel);

            DataContext = _viewModel;
            _viewModel.Registers.RegistersUpdated += Registers_RegistersUpdated;
            _viewModel.Pins.PropertyChanged += Pins_PropertyChanged;
        }

        private void GoToAddressButton_Click(object sender, RoutedEventArgs e)
        {
            _debugger.RequestForRegisters();
            _debugger.RequestForPins();
            _debugger.RequestForCycles();


            /*
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
}";*/
        }

        private async void ConnectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new ConnectWindow(_settings);
            if (window.ShowDialog() == true)
            {
                await InitializeSession(false);
            }
        }

        private async void RunAndConnectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new RunAndConnectWindow(_settings);
            if (window.ShowDialog() == true)
            {
                await InitializeSession(true);
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

        private async Task InitializeSession(bool runEmulator)
        {
            if (runEmulator)
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
            }
            
            StatusLabel.Text = $"Status: connecting to {_settings.Data.Address}...";

            var result = await _debugger.Connect(_settings.Data.Address);
            if (!result.Success)
            {
                StatusLabel.Text = "Status: connection error";
                MessageBox.Show(result.ErrorMessage, "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StatusLabel.Text = $"Status: connected to {_settings.Data.Address}";
            await Task.Delay(100);

            _debugger.RequestForRegisters();
            _debugger.RequestForPins();
            _debugger.RequestForCycles();
        }

        private void Registers_RegistersUpdated(object sender, EventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.UpdateRegisters();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private void Pins_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.UpdatePins();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private void DisplayConnectionError()
        {
            StatusLabel.Text = "Status: connection error";
            MessageBox.Show("Monitor is not connected to the debugger", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
