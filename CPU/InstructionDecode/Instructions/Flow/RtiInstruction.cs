using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// ReTurn from Interrupt
    /// </summary>
    public class RtiInstruction : InstructionBase
    {
        public RtiInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("RTI", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            var low = Core.Bus.Read(Core.Registers.StackPointer);
            Core.Registers.StackPointer++;

            // 1 cycle
            var high = Core.Bus.Read(Core.Registers.StackPointer) << 8;
            Core.Registers.StackPointer++;

            // 1 cycle
            var returnAddress = (ushort)(high | low);
            Core.YieldCycle();

            // 1 cycle
            var statusFlags = Core.Bus.Read(Core.Registers.StackPointer);
            Core.Registers.StackPointer++;

            // 1 cycle
            Core.Registers.ProgramCounter = returnAddress;
            Core.Registers.Flags = (StatusFlags)statusFlags;
            Core.YieldCycle();
        }
    }
}