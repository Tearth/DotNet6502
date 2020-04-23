using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// STore X register
    /// </summary>
    public class StxInstruction : InstructionBase
    {
        public StxInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("STX", opCode, addressingMode, core)
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
        protected override void ExecuteInZeroPageYMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageYMode();

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
            Core.Bus.Write(address, Core.Registers.IndexRegisterX);
        }
    }
}
