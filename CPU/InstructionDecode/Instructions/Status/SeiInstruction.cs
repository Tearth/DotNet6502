using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// SEt Interrupt
    /// </summary>
    public class SeiInstruction : InstructionBase
    {
        public SeiInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("SEI", opCode, addressingMode, core)
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