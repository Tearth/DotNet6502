namespace M6502.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// No OPeration
    /// </summary>
    public class NopInstruction : InstructionBase
    {
        public NopInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("NOP", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycles
            Core.YieldCycle();
        }
    }
}