using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Branch
{
    /// <summary>
    /// Branch on EQual
    /// </summary>
    public class BeqInstruction : InstructionBase
    {
        public BeqInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("BEQ", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected override void ExecuteInRelativeMode()
        {
            // 1 cycle
            var test = Core.Bus.Read(Core.Registers.ProgramCounter);
            var relativeAddress = (sbyte)Core.Bus.Read(Core.Registers.ProgramCounter);
            Core.Registers.ProgramCounter++;

            var oldProgramCounter = Core.Registers.ProgramCounter;
            if (Core.Registers.Flags.HasFlag(StatusFlags.Zero))
            {
                // 1 cycle
                Core.Registers.ProgramCounter = (ushort)(Core.Registers.ProgramCounter + relativeAddress);
                Core.YieldCycle();

                // 1 cycle if page boundary crossed
                if ((oldProgramCounter & 0xFF00) != (Core.Registers.ProgramCounter & 0xFF00))
                {
                    Core.YieldCycle();
                }
            }
        }
    }
}