namespace CPU.Registers
{
    public class RegistersState
    {
        /// <summary>
        /// 16-bit register containing address of the executing instruction.
        /// </summary>
        public ushort ProgramCounter { get; set; }

        /// <summary>
        /// 8-bit register containing address of the stack pointer.
        /// </summary>
        public byte StackPointer { get; set; }

        /// <summary>
        /// 8-bit register used for arithmetic operations.
        /// </summary>
        public byte Accumulator { get; set; }

        /// <summary>
        /// 8-bit register used in various memory addressing modes.
        /// </summary>
        public byte IndexRegisterX { get; set; }

        /// <summary>
        /// 8-bit register used in various memory addressing modes.
        /// </summary>
        public byte IndexRegisterY { get; set; }

        /// <summary>
        /// 8-bit register containing processor status.
        /// </summary>
        public StatusFlags Flags { get; set; }

        public RegistersState()
        {
            Flags = StatusFlags.Reserved;
        }

        public void ChangeFlag(StatusFlags flag, bool value)
        {
            Flags = (StatusFlags) ((byte) Flags & ~(byte)flag);
            if (value)
            {
                Flags |= flag;
            }
        }
    }
}
