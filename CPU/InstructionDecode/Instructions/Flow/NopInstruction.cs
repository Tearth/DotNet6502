﻿using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// No OPeration
    /// </summary>
    public class NopInstruction : InstructionBase
    {
        public NopInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("NOP", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycles
            Core.YieldCycle();
        }
    }
}