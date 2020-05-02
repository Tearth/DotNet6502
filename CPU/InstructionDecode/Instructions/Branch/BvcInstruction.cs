using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on oVerflow Clear
    /// </summary>
    public class BvcInstruction : BranchInstructionBase
    {
        public BvcInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("BVC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch((Core.Registers.Flags & StatusFlags.Overflow) == 0);
        }
    }
}