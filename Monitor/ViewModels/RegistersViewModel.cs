using System.ComponentModel;

namespace Monitor.ViewModels
{
    public class RegistersViewModel : INotifyPropertyChanged
    {
        private ushort _pc;
        public ushort Pc
        {
            get => _pc;
            set
            {
                _pc = value;
                OnPropertyChanged("Pc");
            }
        }

        private byte _sp;
        public byte Sp
        {
            get => _sp;
            set
            {
                _sp = value;
                OnPropertyChanged("Sp");
            }
        }

        private byte _acc;
        public byte Acc
        {
            get => _acc;
            set
            {
                _acc = value;
                OnPropertyChanged("Acc");
            }
        }

        private byte _x;
        public byte X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        private byte _y;
        public byte Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        private byte _flags;
        public byte Flags
        {
            get => _flags;
            set
            {
                _flags = value;
                OnPropertyChanged("Flags");
            }
        }

        private bool _carry;
        public bool Carry
        {
            get => _carry;
            set
            {
                _carry = value;
                OnPropertyChanged("Carry");
            }
        }

        private bool _zero;
        public bool Zero
        {
            get => _zero;
            set
            {
                _zero = value;
                OnPropertyChanged("Zero");
            }
        }

        private bool _interruptsOff;
        public bool InterruptsOff
        {
            get => _interruptsOff;
            set
            {
                _interruptsOff = value;
                OnPropertyChanged("InterruptsOff");
            }
        }

        private bool _decimalMode;
        public bool DecimalMode
        {
            get => _decimalMode;
            set
            {
                _decimalMode = value;
                OnPropertyChanged("DecimalMode");
            }
        }

        private bool _brkInterrupt;
        public bool BrkInterrupt
        {
            get => _brkInterrupt;
            set
            {
                _brkInterrupt = value;
                OnPropertyChanged("BrkInterrupt");
            }
        }

        private bool _overflow;
        public bool Overflow
        {
            get => _overflow;
            set
            {
                _overflow = value;
                OnPropertyChanged("Overflow");
            }
        }

        private bool _signed;
        public bool Signed
        {
            get => _signed;
            set
            {
                _signed = value;
                OnPropertyChanged("Signed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
