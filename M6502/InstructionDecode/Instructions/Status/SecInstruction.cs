using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// SEt Carry
    /// </summary>
    public class SecInstruction : InstructionBase
    {
        public SecInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("SEC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.Carry, true);
            Core.YieldCycle();
        }
    }
}