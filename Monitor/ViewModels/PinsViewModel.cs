using System.ComponentModel;

namespace Monitor.ViewModels
{
    public class PinsViewModel : INotifyPropertyChanged
    {
        private bool _vcc;
        public bool Vcc
        {
            get => _vcc;
            set
            {
                _vcc = value;
                OnPropertyChanged("Vcc");
            }
        }

        private bool _rdy;
        public bool Rdy
        {
            get => _rdy;
            set
            {
                _rdy = value;
                OnPropertyChanged("Rdy");
            }
        }

        private bool _irq;
        public bool Irq
        {
            get => _irq;
            set
            {
                _irq = value;
                OnPropertyChanged("Irq");
            }
        }

        private bool _nmi;
        public bool Nmi
        {
            get => _nmi;
            set
            {
                _nmi = value;
                OnPropertyChanged("Nmi");
            }
        }

        private bool _sync;
        public bool Sync
        {
            get => _sync;
            set
            {
                _sync = value;
                OnPropertyChanged("Sync");
            }
        }

        private bool _a0;
        public bool A0
        {
            get => _a0;
            set
            {
                _a0 = value;
                OnPropertyChanged("A0");
            }
        }

        private bool _a1;
        public bool A1
        {
            get => _a1;
            set
            {
                _a1 = value;
                OnPropertyChanged("A1");
            }
        }

        private bool _a2;
        public bool A2
        {
            get => _a2;
            set
            {
                _a2 = value;
                OnPropertyChanged("A2");
            }
        }

        private bool _a3;
        public bool A3
        {
            get => _a3;
            set
            {
                _a3 = value;
                OnPropertyChanged("A3");
            }
        }

        private bool _a4;
        public bool A4
        {
            get => _a4;
            set
            {
                _a4 = value;
                OnPropertyChanged("A4");
            }
        }

        private bool _a5;
        public bool A5
        {
            get => _a5;
            set
            {
                _a5 = value;
                OnPropertyChanged("A5");
            }
        }

        private bool _a6;
        public bool A6
        {
            get => _a6;
            set
            {
                _a6 = value;
                OnPropertyChanged("A6");
            }
        }

        private bool _a7;
        public bool A7
        {
            get => _a7;
            set
            {
                _a7 = value;
                OnPropertyChanged("A7");
            }
        }

        private bool _a8;
        public bool A8
        {
            get => _a8;
            set
            {
                _a8 = value;
                OnPropertyChanged("A8");
            }
        }

        private bool _a9;
        public bool A9
        {
            get => _a9;
            set
            {
                _a9 = value;
                OnPropertyChanged("A9");
            }
        }

        private bool _a10;
        public bool A10
        {
            get => _a10;
            set
            {
                _a10 = value;
                OnPropertyChanged("A10");
            }
        }

        private bool _a11;
        public bool A11
        {
            get => _a11;
            set
            {
                _a11 = value;
                OnPropertyChanged("A11");
            }
        }

        private bool _a12;
        public bool A12
        {
            get => _a12;
            set
            {
                _a12 = value;
                OnPropertyChanged("A12");
            }
        }

        private bool _a13;
        public bool A13
        {
            get => _a13;
            set
            {
                _a13 = value;
                OnPropertyChanged("A13");
            }
        }

        private bool _a14;
        public bool A14
        {
            get => _a14;
            set
            {
                _a14 = value;
                OnPropertyChanged("A14");
            }
        }

        private bool _a15;
        public bool A15
        {
            get => _a15;
            set
            {
                _a15 = value;
                OnPropertyChanged("A15");
            }
        }

        private bool _res;
        public bool Res
        {
            get => _res;
            set
            {
                _res = value;
                OnPropertyChanged("Res");
            }
        }

        private bool _rw;
        public bool Rw
        {
            get => _rw;
            set
            {
                _rw = value;
                OnPropertyChanged("Rw");
            }
        }

        private bool _d0;
        public bool D0
        {
            get => _d0;
            set
            {
                _d0 = value;
                OnPropertyChanged("D0");
            }
        }

        private bool _d1;
        public bool D1
        {
            get => _d1;
            set
            {
                _d1 = value;
                OnPropertyChanged("D1");
            }
        }

        private bool _d2;
        public bool D2
        {
            get => _d2;
            set
            {
                _d2 = value;
                OnPropertyChanged("D2");
            }
        }

        private bool _d3;
        public bool D3
        {
            get => _d3;
            set
            {
                _d3 = value;
                OnPropertyChanged("D3");
            }
        }

        private bool _d4;
        public bool D4
        {
            get => _d4;
            set
            {
                _d4 = value;
                OnPropertyChanged("D4");
            }
        }

        private bool _d5;
        public bool D5
        {
            get => _d5;
            set
            {
                _d5 = value;
                OnPropertyChanged("D5");
            }
        }

        private bool _d6;
        public bool D6
        {
            get => _d6;
            set
            {
                _d6 = value;
                OnPropertyChanged("D6");
            }
        }

        private bool _d7;
        public bool D7
        {
            get => _d7;
            set
            {
                _d7 = value;
                OnPropertyChanged("D7");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
