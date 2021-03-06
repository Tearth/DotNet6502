﻿using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// CLear Interrupt
    /// </summary>
    public class CliInstruction : InstructionBase
    {
        public CliInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("CLI", opCode, addressingMode, core)
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