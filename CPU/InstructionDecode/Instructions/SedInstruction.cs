﻿using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions
{
    /// <summary>
    /// SEt Decimal
    /// </summary>
    public class SedInstruction : InstructionBase
    {
        public SedInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("SED", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.DecimalMode, true);
            Core.YieldCycle();
        }
    }
}