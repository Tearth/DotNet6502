using System.ComponentModel;

namespace Monitor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public PinsViewModel Pins { get; set; } = new PinsViewModel();
        public RegistersViewModel Registers { get; set; } = new RegistersViewModel();
        public bool Locked { get; set; }

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

        private byte[] _disassembledCode;
        public byte[] DisassembledCode
        {
            get => _disassembledCode;
            set
            {
                _disassembledCode = value;
                OnPropertyChanged("DisassembledCode");
            }
        }

        private byte[] _memory;
        public byte[] Memory
        {
            get => _memory;
            set
            {
                _memory = value;
                OnPropertyChanged("Memory");
            }
        }

        private ushort _memoryAddress;
        public ushort MemoryAddress
        {
            get => _memoryAddress;
            set
            {
                _memoryAddress = value;
                OnPropertyChanged("MemoryAddress");
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
