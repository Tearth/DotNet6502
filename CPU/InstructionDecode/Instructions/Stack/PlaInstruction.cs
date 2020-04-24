namespace CPU.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// PuLl Accumulator
    /// </summary>
    public class PlaInstruction : InstructionBase
    {
        public PlaInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("PLA", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            var value = Core.Bus.Read(Core.Registers.StackPointer);
            Core.Registers.StackPointer++;

            // 1 cycle
            Core.YieldCycle();

            // 1 cycle
            Core.Registers.Accumulator = value;
            Core.YieldCycle();
        }
    }
}