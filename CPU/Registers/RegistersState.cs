namespace CPU.Registers
{
    public class RegistersState
    {
        public ushort ProgramCounter { get; set; }
        public byte StackPointer { get; set; }
        public byte Accumulator { get; set; }
        public byte IndexRegisterX { get; set; }
        public byte IndexRegisterY { get; set; }
        public FlagRegister Flags { get; set; }

        public RegistersState()
        {
            Flags = new FlagRegister();
        }
    }
}
