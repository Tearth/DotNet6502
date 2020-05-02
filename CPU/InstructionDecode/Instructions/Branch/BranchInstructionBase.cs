namespace CPU.InstructionDecode.Instructions.Branch
{
    public abstract class BranchInstructionBase : InstructionBase
    {
        protected BranchInstructionBase(string name, byte opCode, AddressingMode addressingMode, Mos6502Core core) : base(name, opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Cycles: 1F + 1 + 1J + 1B.
        /// </summary>
        protected void DoBranch(bool condition)
        {
            // 1 cycle
            var relativeAddress = (sbyte)Core.Bus.Read(Core.Registers.ProgramCounter);
            Core.Registers.ProgramCounter++;

            var oldProgramCounter = Core.Registers.ProgramCounter;
            if (condition)
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