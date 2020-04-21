using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions
{
    /// <summary>
    /// ADd with Carry
    /// </summary>
    public class SbcInstruction : InstructionBase
    {
        public SbcInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("SBC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            SubWithCarry(Core.Registers.ProgramCounter++);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 2.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 1 cycle
            SubWithCarry(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            SubWithCarry(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            SubWithCarry(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteXMode();

            // 1 cycle
            SubWithCarry(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteYMode();

            // 1 cycle
            SubWithCarry(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndexedIndirectMode();

            // 1 cycle
            SubWithCarry(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4 + 1B.
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            // 3 cycles + 1 if page boundary crossed
            var address = ReadAddressInIndirectIndexedMode();

            // 1 cycle
            SubWithCarry(address);
        }

        private void SubWithCarry(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            var a = Core.Registers.Accumulator;
            var c = Core.Registers.Flags.HasFlag(StatusFlags.Carry) ? 0 : 1;
            var result = a - number - c;

            Core.Registers.Accumulator = (byte)result;

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = (result & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            var carryFlag = result > byte.MaxValue || result < byte.MinValue;
            Core.Registers.ChangeFlag(StatusFlags.Carry, carryFlag);

            var overflowFlag = ((a ^ (sbyte)result) & (number ^ (sbyte)result) & 0x80) != 0;
            Core.Registers.ChangeFlag(StatusFlags.Overflow, overflowFlag);
        }
    }
}
