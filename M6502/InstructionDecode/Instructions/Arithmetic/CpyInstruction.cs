using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// ComPare Y register
    /// </summary>
    public class CpyInstruction : InstructionBase
    {
        public CpyInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("CPY", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            LoadAndDoCmp(Core.Registers.ProgramCounter);
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
            LoadAndDoCmp(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            LoadAndDoCmp(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void LoadAndDoCmp(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            DoCmp(number);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private void DoCmp(byte number)
        {
            var result = (byte)(Core.Registers.IndexRegisterY - number);

            var zeroFlag = Core.Registers.IndexRegisterY == number;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((result >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            var carryFlag = Core.Registers.IndexRegisterY >= number;
            Core.Registers.ChangeFlag(StatusFlags.Carry, carryFlag);
        }
    }
}
