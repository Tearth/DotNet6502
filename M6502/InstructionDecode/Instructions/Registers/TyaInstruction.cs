﻿using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// Transfer X to A
    /// </summary>
    public class TyaInstruction : InstructionBase
    {
        public TyaInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("TYA", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.Accumulator = Core.Registers.IndexRegisterY;

            var zeroFlag = Core.Registers.Accumulator == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.Accumulator >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}