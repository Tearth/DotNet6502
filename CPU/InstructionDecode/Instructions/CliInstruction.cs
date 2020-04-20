using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions
{
    /// <summary>
    /// CLear Interrupt
    /// </summary>
    public class CliInstruction : InstructionBase
    {
        public CliInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("CLI", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.IrqDisable, false);
            Core.YieldCycle();
        }
    }
}