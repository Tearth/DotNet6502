using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on oVerflow Set
    /// </summary>
    public class BvsInstruction : BranchInstructionBase
    {
        public BvsInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("BVS", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch((Core.Registers.Flags & StatusFlags.Overflow) != 0);
        }
    }
}