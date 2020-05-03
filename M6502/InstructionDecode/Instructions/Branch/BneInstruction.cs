using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on Not Equal
    /// </summary>
    public class BneInstruction : BranchInstructionBase
    {
        public BneInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("BNE", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch((Core.Registers.Flags & StatusFlags.Zero) == 0);
        }
    }
}