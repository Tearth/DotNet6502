using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// SEt Interrupt
    /// </summary>
    public class SeiInstruction : InstructionBase
    {
        public SeiInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("SEI", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.IrqDisable, true);
            Core.YieldCycle();
        }
    }
}