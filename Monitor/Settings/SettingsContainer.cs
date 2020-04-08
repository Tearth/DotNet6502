using System.IO;
using Newtonsoft.Json;

namespace Monitor.Settings
{
    public class SettingsContainer
    {
        public SettingsData Data { get; private set; }
        private readonly string _filePath;

        public SettingsContainer(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            if (File.Exists(_filePath))
            {
                var content = File.ReadAllText(_filePath);
                Data = JsonConvert.DeserializeObject<SettingsData>(content);
            }
            else
            {
                Data = new SettingsData();
            }
        }

        public void Save()
        {
            var content = JsonConvert.SerializeObject(Data);
            File.WriteAllText(_filePath, content);
        }
    }
}
