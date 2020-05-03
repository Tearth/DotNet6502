using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// CLear Carry
    /// </summary>
    public class ClcInstruction : InstructionBase
    {
        public ClcInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("CLC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.Carry, false);
            Core.YieldCycle();
        }
    }
}