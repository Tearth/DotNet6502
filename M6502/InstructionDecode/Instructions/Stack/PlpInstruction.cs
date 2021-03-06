﻿using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// PuLl Processor status
    /// </summary>
    public class PlpInstruction : InstructionBase
    {
        public PlpInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("PLP", opCode, addressingMode, core)
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
            Core.Registers.Flags = (StatusFlags)value;
            Core.YieldCycle();
        }
    }
}