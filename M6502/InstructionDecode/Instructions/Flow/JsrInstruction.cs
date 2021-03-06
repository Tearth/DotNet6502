﻿namespace M6502.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// Jump to SubRoutine
    /// </summary>
    public class JsrInstruction : InstructionBase
    {
        public JsrInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("JSR", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycles
            var returnAddress = (ushort)(Core.Registers.ProgramCounter - 1);
            Core.YieldCycle();

            // 1 cycle
            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), (byte)(returnAddress >> 8));
            Core.Registers.StackPointer--;

            // 1 cycle
            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), (byte)returnAddress);
            Core.Registers.StackPointer--;

            Core.Registers.ProgramCounter = address;
        }
    }
}