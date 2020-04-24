namespace CPU.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// PuLl Accumulator
    /// </summary>
    public class PhpInstruction : InstructionBase
    {
        public PhpInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("PHP", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Bus.Write(Core.Registers.StackPointer, (byte)Core.Registers.Flags);
            Core.Registers.StackPointer--;

            // 1 cycle
            Core.YieldCycle();
        }
    }
}