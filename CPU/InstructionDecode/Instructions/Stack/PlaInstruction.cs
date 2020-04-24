using CPU.Registers;

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
            Core.Registers.StackPointer++;
            var value = Core.Bus.Read((ushort)(0x100 + Core.Registers.StackPointer));

            // 1 cycle
            Core.YieldCycle();

            // 1 cycle
            Core.Registers.Accumulator = value;

            var zeroFlag = Core.Registers.Accumulator == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.Accumulator >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}