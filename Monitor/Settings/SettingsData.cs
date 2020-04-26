using System.ComponentModel;

namespace Monitor.Settings
{
    public class SettingsData : INotifyPropertyChanged
    {
        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        private string _arguments;
        public string Arguments
        {
            get => _arguments;
            set
            {
                _arguments = value;
                OnPropertyChanged("Arguments");
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        private ushort _runToAddress;
        public ushort RunToAddress
        {
            get => _runToAddress;
            set
            {
                _runToAddress = value;
                OnPropertyChanged("RunToAddress");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
