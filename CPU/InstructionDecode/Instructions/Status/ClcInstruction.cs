using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// CLear Carry
    /// </summary>
    public class ClcInstruction : InstructionBase
    {
        public ClcInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("CLC", opCode, addressingMode, core)
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