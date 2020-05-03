using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// SEt Decimal
    /// </summary>
    public class SedInstruction : InstructionBase
    {
        public SedInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("SED", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.DecimalMode, true);
            Core.YieldCycle();
        }
    }
}