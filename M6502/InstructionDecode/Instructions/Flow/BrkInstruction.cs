namespace M6502.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// BReaK
    /// </summary>
    public class BrkInstruction : InstructionBase
    {
        public BrkInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("BRK", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 6.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 6 cycles
            Core.RequestBrk();
        }
    }
}