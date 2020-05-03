namespace M6502.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// JuMP
    /// </summary>
    public class JmpInstruction : InstructionBase
    {
        public JmpInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("JMP", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            Core.Registers.ProgramCounter = address;
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndirectMode();

            Core.Registers.ProgramCounter = address;
        }
    }
}
