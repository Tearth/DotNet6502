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

        private void GoToAddressButton_Click(object sender, RoutedEventArgs e)
        {
            _debugger.RequestForMemory();
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

            await RequestForAllData();
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

        private void DisplayConnectionError()
        {
            StatusLabel.Text = "Status: connection error";
            MessageBox.Show("Monitor is not connected to the debugger", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            _debugger.SendStopCommand();
            await RequestForAllData();
        }

        private void ContinueButton_OnClick(object sender, RoutedEventArgs e)
        {
            _debugger.SendContinueCommand();
        }

        private async void NextInstructionButton_OnClick(object sender, RoutedEventArgs e)
        {
            _debugger.SendNextInstructionCommand();
            await RequestForAllData();
        }

        private async void NextCycleButton_OnClick(object sender, RoutedEventArgs e)
        {
            _debugger.SendNextCycleCommand();
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
    }
}
