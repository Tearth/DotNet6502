using CommandLine;

namespace Host
{
    public class CommandLineOptions
    {
        [Option('f', "frequency", Required = true, HelpText = "Set processor frequency (Hz).")]
        public uint Frequency { get; set; }
    }
}