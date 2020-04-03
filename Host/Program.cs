using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Text.RegularExpressions;
using CommandLine;
using CPU;
using CPU.IO;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(settings =>
            {
                settings.CaseSensitive = false;
                settings.HelpWriter = Console.Error;
                settings.IgnoreUnknownArguments = true;
            });

            var result = parser.ParseArguments<CommandLineOptions>(args);
            result.WithParsed(options =>
            {
                var devices = ParseDevices(string.Join(" ", args));
                var loadedDevices = LoadDevices(devices);

                var emulator = new Mos6502Core(options.Frequency);
                loadedDevices.ForEach(d => emulator.AttachDeviceToBus(d));

                emulator.Run();
            });
        }

        private static List<DeviceDefinition> ParseDevices(string args)
        {
            var definitionsList = new List<DeviceDefinition>();
            var matches = Regex.Matches(args, @"\-d (?<dllName>.*?)\[(?<parameters>.*?)\]");

            foreach (Match match in matches)
            {
                var dllName = match.Groups["dllName"].Captures.First().Value;
                var parameters = match.Groups["parameters"].Captures.First().Value;

                definitionsList.Add(new DeviceDefinition(dllName, parameters));
            }

            return definitionsList;
        }

        private static List<IDevice> LoadDevices(List<DeviceDefinition> definitions)
        {
            var devices = new List<IDevice>();
            foreach (var definition in definitions)
            {
                if (!File.Exists(definition.AbsoluteDllNameWithExtension))
                {
                    throw new DllNotFoundException($"Can't find device DLL ({definition.DllNameWithExtension}).");
                }

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(definition.AbsoluteDllNameWithExtension);
                var assemblyTypes = assembly.GetTypes();
                var deviceInterfaceType = typeof(IDevice);

                var deviceClassType = assemblyTypes.FirstOrDefault(d => deviceInterfaceType.IsAssignableFrom(d));
                if (deviceClassType == null)
                {
                    throw new EntryPointNotFoundException($"Can't find device class ({definition.DllNameWithExtension}).");
                }

                var deviceInstance = (IDevice)Activator.CreateInstance(deviceClassType);
                if (!deviceInstance.Configure(definition.SplitParameters))
                {
                    throw new InvalidOperationException($"Can't configure device class ({definition.DllNameWithExtension}).");
                }

                devices.Add(deviceInstance);
            }

            return devices;
        }
    }
}
