using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on Carry Clear
    /// </summary>
    public class BccInstruction : BranchInstructionBase
    {
        public BccInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("BCC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch((Core.Registers.Flags & StatusFlags.Carry) == 0);
        }
    }
}