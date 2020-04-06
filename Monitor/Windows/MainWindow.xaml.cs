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
            
        }
    }
}
