namespace Host
{
    public class DeviceDefinition
    {
        public string DllPath { get; set; }
        public string DllPathWithExtension => $"{DllPath}.dll";
        public string Parameters { get; set; }

        public DeviceDefinition(string dllPath, string parameters)
        {
            DllPath = dllPath;
            Parameters = parameters;
        }
    }
}
