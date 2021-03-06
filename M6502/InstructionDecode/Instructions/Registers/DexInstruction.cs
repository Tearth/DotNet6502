﻿using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// DEcrement X
    /// </summary>
    public class DexInstruction : InstructionBase
    {
        public DexInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("DEX", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.IndexRegisterX--;

            var zeroFlag = Core.Registers.IndexRegisterX == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.IndexRegisterX >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}