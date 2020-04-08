using CommandLine;

namespace Host
{
    public class CommandLineOptions
    {
        [Option('f', "frequency", Required = true, HelpText = "Set processor frequency (Hz).")]
        public uint Frequency { get; set; }

        [Option('d', "debugger", Required = false, HelpText = "Enable or disable debugger server.", Default = false)]
        public bool IsDebuggerEnabled { get; set; }

        [Option('p', "port", Required = false, HelpText = "Set debugger server port.", Default = (ushort)6502)]
        public ushort DebuggerPort { get; set; }

        [Option('w', "wait", Required = false, HelpText = "Wait for debugger (set RDY pin to 0).", Default = false)]
        public bool WaitForDebugger { get; set; }
    }
}