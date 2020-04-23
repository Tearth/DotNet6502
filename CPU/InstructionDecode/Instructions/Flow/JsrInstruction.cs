using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// Jump to SubRoutine
    /// </summary>
    public class JsrInstruction : InstructionBase
    {
        public JsrInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("JSR", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycles
            var returnAddress = (ushort)(Core.Registers.ProgramCounter - 1);
            Core.YieldCycle();

            // 1 cycle
            Core.Bus.Write(Core.Registers.StackPointer, (byte)(returnAddress >> 8));
            Core.Registers.StackPointer--;

            // 1 cycle
            Core.Bus.Write(Core.Registers.StackPointer, (byte)returnAddress);
            Core.Registers.StackPointer--;

            Core.Registers.ProgramCounter = address;
        }
    }
}