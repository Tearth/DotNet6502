﻿using System.ComponentModel;

namespace Monitor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public PinsViewModel Pins { get; set; } = new PinsViewModel();
        public RegistersViewModel Registers { get; set; } = new RegistersViewModel();

        private string _stack;
        public string Stack
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
