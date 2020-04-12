using System;
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
                OnPropertyChanged("Carry");
                OnPropertyChanged("Zero");
                OnPropertyChanged("InterruptsOff");
                OnPropertyChanged("DecimalMode");
                OnPropertyChanged("BrkInterrupt");
                OnPropertyChanged("Overflow");
                OnPropertyChanged("Signed");
            }
        }

        public bool Carry
        {
            get => Convert.ToBoolean((Flags >> 7) & 1);
            set
            {
                Flags = ChangeBit(Flags, 7, value);
                OnPropertyChanged("Carry");
            }
        }

        public bool Zero
        {
            get => Convert.ToBoolean((Flags >> 6) & 1);
            set
            {
                Flags = ChangeBit(Flags, 6, value);
                OnPropertyChanged("Zero");
            }
        }

        public bool InterruptsOff
        {
            get => Convert.ToBoolean((Flags >> 5) & 1);
            set
            {
                Flags = ChangeBit(Flags, 5, value);
                OnPropertyChanged("InterruptsOff");
            }
        }

        public bool DecimalMode
        {
            get => Convert.ToBoolean((Flags >> 4) & 1);
            set
            {
                Flags = ChangeBit(Flags, 4, value);
                OnPropertyChanged("DecimalMode");
            }
        }

        public bool BrkInterrupt
        {
            get => Convert.ToBoolean((Flags >> 3) & 1);
            set
            {
                Flags = ChangeBit(Flags, 3, value);
                OnPropertyChanged("BrkInterrupt");
            }
        }

        public bool Overflow
        {
            get => Convert.ToBoolean((Flags >> 1) & 1);
            set
            {
                Flags = ChangeBit(Flags, 1, value);
                OnPropertyChanged("Overflow");
            }
        }

        public bool Signed
        {
            get => Convert.ToBoolean((Flags >> 0) & 1);
            set
            {
                Flags = ChangeBit(Flags, 0, value);
                OnPropertyChanged("Signed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private byte ChangeBit(byte val, int bitIndex, bool bitValue)
        {
            return (byte)((val & ~(1 << bitIndex)) | ((bitValue ? 1 : 0) << bitIndex));
        }
    }
}
