using System;
using System.ComponentModel;
using Monitor.Helpers;

namespace Monitor.ViewModels
{
    public class RegistersViewModel : INotifyPropertyChanged
    {
        private ushort _programCounter;
        public ushort ProgramCounter
        {
            get => _programCounter;
            set
            {
                _programCounter = value;
                OnPropertyChanged("ProgramCounter");
                RegistersUpdated?.Invoke(this, null);
            }
        }

        private byte _stackPointer;
        public byte StackPointer
        {
            get => _stackPointer;
            set
            {
                _stackPointer = value;
                OnPropertyChanged("StackPointer");
                RegistersUpdated?.Invoke(this, null);
            }
        }

        private byte _accumulator;
        public byte Accumulator
        {
            get => _accumulator;
            set
            {
                _accumulator = value;
                OnPropertyChanged("Accumulator");
                RegistersUpdated?.Invoke(this, null);
            }
        }

        private byte _indexRegisterX;
        public byte IndexRegisterX
        {
            get => _indexRegisterX;
            set
            {
                _indexRegisterX = value;
                OnPropertyChanged("IndexRegisterX");
                RegistersUpdated?.Invoke(this, null);
            }
        }

        private byte _indexRegisterY;
        public byte IndexRegisterY
        {
            get => _indexRegisterY;
            set
            {
                _indexRegisterY = value;
                OnPropertyChanged("IndexRegisterY");
                RegistersUpdated?.Invoke(this, null);
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
                RegistersUpdated?.Invoke(this, null);
            }
        }

        public bool Carry
        {
            get => Convert.ToBoolean((Flags >> 0) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 0, value);
                OnPropertyChanged("Carry");
            }
        }

        public bool Zero
        {
            get => Convert.ToBoolean((Flags >> 1) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 1, value);
                OnPropertyChanged("Zero");
            }
        }

        public bool InterruptsOff
        {
            get => Convert.ToBoolean((Flags >> 2) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 2, value);
                OnPropertyChanged("InterruptsOff");
            }
        }

        public bool DecimalMode
        {
            get => Convert.ToBoolean((Flags >> 3) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 3, value);
                OnPropertyChanged("DecimalMode");
            }
        }

        public bool BrkInterrupt
        {
            get => Convert.ToBoolean((Flags >> 4) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 4, value);
                OnPropertyChanged("BrkInterrupt");
            }
        }

        public bool Overflow
        {
            get => Convert.ToBoolean((Flags >> 6) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 6, value);
                OnPropertyChanged("Overflow");
            }
        }

        public bool Signed
        {
            get => Convert.ToBoolean((Flags >> 7) & 1);
            set
            {
                Flags = BitHelpers.ChangeBit(Flags, 7, value);
                OnPropertyChanged("Signed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RegistersUpdated;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
