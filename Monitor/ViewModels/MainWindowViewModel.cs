using System.ComponentModel;

namespace Monitor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public PinsViewModel Pins { get; set; } = new PinsViewModel();
        public RegistersViewModel Registers { get; set; } = new RegistersViewModel();

        private byte[] _stack;
        public byte[] Stack
        {
            get => _stack;
            set
            {
                _stack = value;
                OnPropertyChanged("Stack");
            }
        }

        private string _disassembledCode;
        public string DisassembledCode
        {
            get => _disassembledCode;
            set
            {
                _disassembledCode = value;
                OnPropertyChanged("DisassembledCode");
            }
        }

        private string _memory;
        public string Memory
        {
            get => _memory;
            set
            {
                _memory = value;
                OnPropertyChanged("Memory");
            }
        }

        private string _bus;
        public string Bus
        {
            get => _bus;
            set
            {
                _bus = value;
                OnPropertyChanged("Bus");
            }
        }

        private ulong _cycles;
        public ulong Cycles
        {
            get => _cycles;
            set
            {
                _cycles = value;
                OnPropertyChanged("Cycles");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
