using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// ReTurn from Subroutine
    /// </summary>
    public class RtsInstruction : InstructionBase
    {
        public RtsInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("RTS", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.StackPointer++;
            var low = Core.Bus.Read((ushort)(0x100 + Core.Registers.StackPointer));

            // 1 cycle
            Core.Registers.StackPointer++;
            var high = Core.Bus.Read((ushort)(0x100 + Core.Registers.StackPointer)) << 8;

            // 1 cycle
            var returnAddress = (ushort) ((high | low) + 1);
            Core.YieldCycle();

            // 2 cycles
            Core.Registers.ProgramCounter = returnAddress;
            Core.YieldCycle();
            Core.YieldCycle();
        }
    }
}