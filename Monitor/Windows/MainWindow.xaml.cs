using System.Windows;
using Monitor.ViewModels;

namespace Monitor.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = new MainWindowViewModel();

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

        private void ConnectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var connectWindow = new ConnectWindow();
            connectWindow.ShowDialog();
        }

        private void RunAndConnectMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
