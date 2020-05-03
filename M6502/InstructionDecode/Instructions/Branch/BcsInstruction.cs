using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on Carry Set
    /// </summary>
    public class BcsInstruction : BranchInstructionBase
    {
        public BcsInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("BCS", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch((Core.Registers.Flags & StatusFlags.Carry) != 0);
        }
    }
}