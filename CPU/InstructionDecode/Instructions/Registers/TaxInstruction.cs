﻿using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// Transfer A to X
    /// </summary>
    public class TaxInstruction : InstructionBase
    {
        public TaxInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("TAX", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.IndexRegisterX = Core.Registers.Accumulator;
            
            var zeroFlag = Core.Registers.IndexRegisterX == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = (Core.Registers.IndexRegisterX & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}