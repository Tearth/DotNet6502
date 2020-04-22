﻿using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// INcrement Y
    /// </summary>
    public class InyInstruction : InstructionBase
    {
        public InyInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("INY", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.IndexRegisterY++;

            var zeroFlag = Core.Registers.IndexRegisterY == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = (Core.Registers.IndexRegisterY & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}