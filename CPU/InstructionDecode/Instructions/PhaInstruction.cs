﻿using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions
{
    /// <summary>
    /// PusH Accumulator
    /// </summary>
    public class PhaInstruction : InstructionBase
    {
        public PhaInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("PHA", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Bus.Write(Core.Registers.StackPointer, Core.Registers.Accumulator);
            Core.YieldCycle();

            // 1 cycle
            Core.Registers.StackPointer--;
            Core.YieldCycle();
        }
    }
}