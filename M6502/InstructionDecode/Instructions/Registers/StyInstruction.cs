namespace M6502.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// STore X register
    /// </summary>
    public class StyInstruction : InstructionBase
    {
        public StyInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("STY", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void Store(ushort address)
        {
            // 1 cycle
            Core.Bus.Write(address, Core.Registers.IndexRegisterY);
        }
    }
}