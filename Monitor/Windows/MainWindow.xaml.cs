using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Monitor.Debugger;
using Monitor.Instructions;
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
            _settings = new SettingsContainer("Settings.json");
            _debugger = new DebuggerClient(_viewModel);

            DataContext = _viewModel;
            _viewModel.Registers.RegistersUpdated += Registers_RegistersUpdated;
            _viewModel.Pins.PropertyChanged += Pins_PropertyChanged;
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

            await RequestForAllData();
        }

        private async Task RequestForAllData()
        {
            await Task.Delay(1);
            _debugger.RequestForCycles();
            _debugger.RequestForRegisters();
            await Task.Delay(1);
            _debugger.RequestForPins();
            _debugger.RequestForStack();
            _debugger.RequestForCode();
            _debugger.RequestForMemory();
        }

        private void DisplayConnectionError()
        {
            StatusLabel.Text = "Status: connection error";
            MessageBox.Show("Monitor is not connected to the debugger", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Registers_RegistersUpdated(object sender, EventArgs e)
        {
            if (!_viewModel.Locked)
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
        }

        private void Pins_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_viewModel.Locked)
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
        }

        private async void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.SendStopCommand();
                await RequestForAllData();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private void ContinueButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.SendContinueCommand();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private async void RunToAddressButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_debugger.Connected)
            {
                var window = new RunToAddressWindow(_settings);
                if (window.ShowDialog() == true)
                {
                    _debugger.RunToAddressCommand(_settings.Data.RunToAddress);
                    await RequestForAllData();
                }
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private async void NextInstructionButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.SendNextInstructionCommand();
                await RequestForAllData();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private async void NextCycleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.SendNextCycleCommand();
                await RequestForAllData();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private void GoToAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (_debugger.Connected)
            {
                _debugger.RequestForMemory();
            }
            else
            {
                DisplayConnectionError();
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
