﻿using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// LoaD X register
    /// </summary>
    public class LdxInstruction : InstructionBase
    {
        public LdxInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("LDX", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            Load(Core.Registers.ProgramCounter);
            Core.Registers.ProgramCounter++;
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageYMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageYMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteYMode();

            // 1 cycle
            Load(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void Load(ushort address)
        {
            // 1 cycle
            Core.Registers.IndexRegisterX = Core.Bus.Read(address);

            var zeroFlag = Core.Registers.IndexRegisterX == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.IndexRegisterX >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);
        }
    }
}
