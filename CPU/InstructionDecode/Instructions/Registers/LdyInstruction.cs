using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// LoaD Y register
    /// </summary>
    public class LdyInstruction : InstructionBase
    {
        public LdyInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("LDY", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            Load(Core.Registers.ProgramCounter);
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
            Load(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteXMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void Load(ushort address)
        {
            // 1 cycle
            Core.Registers.IndexRegisterY = Core.Bus.Read(address);

            var zeroFlag = Core.Registers.IndexRegisterY == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.IndexRegisterY >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);
        }
    }
}
