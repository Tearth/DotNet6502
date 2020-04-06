using System.ComponentModel;

namespace Monitor.ViewModels
{
    public class MainWindowViewModel
    {
        public PinsViewModel Pins { get; set; } = new PinsViewModel();
    }
}
