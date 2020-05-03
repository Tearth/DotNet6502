namespace M6502.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// PusH Processor status
    /// </summary>
    public class PhpInstruction : InstructionBase
    {
        public PhpInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("PHP", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), (byte)Core.Registers.Flags);
            Core.Registers.StackPointer--;

            // 1 cycle
            Core.YieldCycle();
        }
    }
}