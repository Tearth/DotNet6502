﻿using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// DECrement memory
    /// </summary>
    public class DecInstruction : InstructionBase
    {
        public DecInstruction(byte opCode, AddressingMode addressingMode, Mos6502Core core) : base("DEC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 3 cycle
            LoadAndDoDecrementation(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 3 cycle
            LoadAndDoDecrementation(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 3 cycle
            LoadAndDoDecrementation(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 6.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 3 cycles
            var address = ReadAddressInAbsoluteXMode(true);

            // 3 cycle
            LoadAndDoDecrementation(address);
        }

        /// <summary>
        /// Cycles: 3.
        /// </summary>
        private void LoadAndDoDecrementation(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            var result = DoDecrementation(number);

            // 1 cycle
            Core.YieldCycle();

            // 1 cycle
            Core.Bus.Write(address, result);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private byte DoDecrementation(byte number)
        {
            var result = (byte)(number - 1);

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((result >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            return result;
        }
    }
}
