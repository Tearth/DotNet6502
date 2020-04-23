using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// STore Accumulator
    /// </summary>
    public class StaInstruction : InstructionBase
    {
        public StaInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("STA", opCode, addressingMode, core)
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
        /// Length: 3, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 3 cycles
            var address = ReadAddressInAbsoluteXMode(true);

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 3 cycles
            var address = ReadAddressInAbsoluteYMode(true);

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndexedIndirectMode();

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            // 4 cycles
            var address = ReadAddressInIndirectIndexedMode(true);

            // 1 cycle
            Store(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void Store(ushort address)
        {
            // 1 cycle
            Core.Bus.Write(address, Core.Registers.Accumulator);
        }
    }
}
