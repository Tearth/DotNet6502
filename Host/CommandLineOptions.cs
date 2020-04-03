using CommandLine;

namespace Host
{
    public class CommandLineOptions
    {
        [Option('f', "frequency", Required = true, HelpText = "Set processor frequency (Hz).")]
        public uint Frequency { get; set; }

        [Option('p', "pc", Required = true, HelpText = "Set initial program counter.")]
        public string ProgramCounter { get; set; }
    }
}