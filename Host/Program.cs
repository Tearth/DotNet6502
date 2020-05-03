using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Text.RegularExpressions;
using System.Timers;
using CommandLine;
using CPU;
using CPU.IO;
using Host.Debugger;

namespace Host
{
    class Program
    {
        private static Mos6502Emulator _emulator;
        private static Timer _debugTimer;
        private static ulong _lastInstructionsCount;

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
                WriteDebugInfo(options.DebugInfo, "Starting 6502 emulator...");
                _emulator = new Mos6502Emulator(options.Frequency);

                var devices = ParseDevices(string.Join(" ", args));
                var loadedDevices = LoadDevices(_emulator.Core, devices);
                loadedDevices.ForEach(d => _emulator.Core.Bus.AttachDevice(d));
                WriteDebugInfo(options.DebugInfo, $"Added {loadedDevices.Count} peripherals");

                DebuggerServer debugger = null;
                if (options.DebuggerEnabled)
                {
                    WriteDebugInfo(options.DebugInfo, $"Starting debugger at {options.DebuggerPort} port...");
                    debugger = new DebuggerServer(_emulator.Core, options.DebuggerPort);
                    debugger.Start();

                    if (options.DebugInfo)
                    {
                        _debugTimer = new Timer(1000);
                        _debugTimer.Elapsed += DebugTimer_Elapsed;
                        _debugTimer.Start();
                    }
                }

                WriteDebugInfo(options.DebugInfo, "Starting 6502 core...");
                _emulator.PowerUp();
                _emulator.SetRdyState(!options.WaitForDebugger);
                _emulator.Reset();
                _emulator.Run();

                WriteDebugInfo(options.DebugInfo, "Closing 6502 emulator...");
                debugger?.Dispose();
            });
        }

        private static void WriteDebugInfo(bool debugInfoOption, string message)
        {
            if (debugInfoOption)
            {
                Console.WriteLine(message);
            }
        }

        private static List<DeviceDefinition> ParseDevices(string args)
        {
            var definitionsList = new List<DeviceDefinition>();
            var matches = Regex.Matches(args, @"\-b (?<dllName>.*?)\[(?<parameters>.*?)\]");

            foreach (Match match in matches)
            {
                var dllName = match.Groups["dllName"].Captures.First().Value;
                var parameters = match.Groups["parameters"].Captures.First().Value;

                definitionsList.Add(new DeviceDefinition(dllName, parameters));
            }

            return definitionsList;
        }

        private static List<IDevice> LoadDevices(Mos6502Core core, List<DeviceDefinition> definitions)
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
                if (!deviceInstance.Configure(core.Pins, definition.SplitParameters))
                {
                    throw new InvalidOperationException($"Can't configure device class ({definition.DllNameWithExtension}).");
                }

                devices.Add(deviceInstance);
            }

            return devices;
        }

        private static void DebugTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var delta = _emulator.Core.Cycles - _lastInstructionsCount;
            _lastInstructionsCount = _emulator.Core.Cycles;

            Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: " +
                              $"{_emulator.Core.Cycles} " +
                              $"({delta} c/s, {(float)delta / 1000000:0.00} MHz)");
        }
    }
}
