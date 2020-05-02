namespace CPU.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// PusH Accumulator
    /// </summary>
    public class PhaInstruction : InstructionBase
    {
        public PhaInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("PHA", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), Core.Registers.Accumulator);
            Core.Registers.StackPointer--;

            // 1 cycle
            Core.YieldCycle();
        }
    }
}