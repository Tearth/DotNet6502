using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// CLear Decimal
    /// </summary>
    public class CldInstruction : InstructionBase
    {
        public CldInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("CLD", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.DecimalMode, false);
            Core.YieldCycle();
        }
    }
}
