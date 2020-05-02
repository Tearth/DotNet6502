using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// bitwise AND with accumulator
    /// </summary>
    public class AndInstruction : InstructionBase
    {
        public AndInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("AND", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            LoadAndDoAnd(Core.Registers.ProgramCounter);
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
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteXMode();

            // 1 cycle
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteYMode();

            // 1 cycle
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndexedIndirectMode();

            // 1 cycle
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4 + 1B.
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            // 3 cycles + 1 if page boundary crossed
            var address = ReadAddressInIndirectIndexedMode();

            // 1 cycle
            LoadAndDoAnd(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void LoadAndDoAnd(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            DoAnd(number);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private void DoAnd(byte number)
        {
            Core.Registers.Accumulator = (byte)(Core.Registers.Accumulator & number);

            var zeroFlag = Core.Registers.Accumulator == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.Accumulator >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);
        }
    }
}
