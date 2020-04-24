using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// bitwise Exclusive OR
    /// </summary>
    public class EorInstruction : InstructionBase
    {
        public EorInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("EOR", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            LoadAndDoExclusiveOr(Core.Registers.ProgramCounter);
            Core.Registers.ProgramCounter++;
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteXMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteYMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndexedIndirectMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4 + 1B.
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            // 3 cycles + 1 if page boundary crossed
            var address = ReadAddressInIndirectIndexedMode();

            // 1 cycle
            LoadAndDoExclusiveOr(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void LoadAndDoExclusiveOr(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            DoExclusiveOr(number);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private void DoExclusiveOr(byte number)
        {
            var result = Core.Registers.Accumulator ^ number;
            Core.Registers.Accumulator = (byte)result;

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = (result & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);
        }
    }
}
