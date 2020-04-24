using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on MInus
    /// </summary>
    public class BmiInstruction : BranchInstructionBase
    {
        public BmiInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("BMI", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            DoBranch(Core.Registers.Flags.HasFlag(StatusFlags.Sign));
        }
    }
}