using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on PLus
    /// </summary>
    public class BplInstruction : BranchInstructionBase
    {
        public BplInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("BPL", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch((Core.Registers.Flags & StatusFlags.Sign) == 0);
        }
    }
}
