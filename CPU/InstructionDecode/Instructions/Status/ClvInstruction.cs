using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// CLear oVerflow
    /// </summary>
    public class ClvInstruction : InstructionBase
    {
        public ClvInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("CLV", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.Overflow, false);
            Core.YieldCycle();
        }
    }
}