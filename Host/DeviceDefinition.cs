using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Host
{
    public class DeviceDefinition
    {
        public string DllName { get; set; }
        public string DllNameWithExtension => $"{DllName}.dll";
        public string AbsoluteDllNameWithExtension => Path.GetFullPath(DllNameWithExtension);
        public string Parameters { get; set; }
        public List<string> SplitParameters => Parameters.Split(',').Select(p => p.Trim()).ToList();

        public DeviceDefinition(string dllName, string parameters)
        {
            DllName = dllName;
            Parameters = parameters;
        }
    }
}
