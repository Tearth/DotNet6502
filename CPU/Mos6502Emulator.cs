namespace CPU
{
    public class Mos6502Emulator
    {
        public Mos6502Core Core { get; set; }

        public Mos6502Emulator(uint frequency)
        {
            Core = new Mos6502Core(frequency);
        }

        public void Run()
        {
            Core.Run();
        }

        public void SetProgramCounter(ushort programCounter)
        {
            Core.Registers.ProgramCounter = programCounter;
        }
    }
}
