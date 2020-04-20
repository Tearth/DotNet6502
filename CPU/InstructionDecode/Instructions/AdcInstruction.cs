using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions
{
    /// <summary>
    /// ADd with Carry
    /// </summary>
    public class AdcInstruction : InstructionBase
    {
        public AdcInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("ADC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 2
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            var number = Core.Bus.Read(Core.Registers.ProgramCounter++);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 2, Cycles: 3
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            var address = ReadAddressInZeroPageMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 2, Cycles: 4
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            var address = ReadAddressInZeroPageXMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 3, Cycles: 4
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            var address = ReadAddressInAbsoluteMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 3, Cycles: 4+
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            var address = ReadAddressInAbsoluteXMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 3, Cycles: 4+
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            var address = ReadAddressInAbsoluteYMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 2, Cycles: 6
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            var address = ReadAddressInIndexedIndirectMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }

        /// <summary>
        /// Length: 2, Cycles: 5+
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            var address = ReadAddressInIndirectIndexedMode();
            var number = Core.Bus.Read(address);
            AddWithCarry(number);
        }
        
        /// <summary>
        /// 1 cycle
        /// </summary>
        private void AddWithCarry(byte number)
        {
            var a = Core.Registers.Accumulator;
            var c = Core.Registers.Flags.HasFlag(StatusFlags.Carry) ? 1 : 0;
            var result = a + number + c;

            Core.Registers.Accumulator = (byte)result;

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = (result & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            var carryFlag = result > byte.MaxValue || result < byte.MinValue;
            Core.Registers.ChangeFlag(StatusFlags.Carry, carryFlag);

            var overflowFlag = ((a ^ (sbyte) result) & (number ^ (sbyte) result) & 0x80) != 0;
            Core.Registers.ChangeFlag(StatusFlags.Overflow, overflowFlag);

            Core.YieldCycle();
        }
    }
}
