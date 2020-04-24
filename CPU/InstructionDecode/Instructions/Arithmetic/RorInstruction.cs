using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// ROtate Right
    /// </summary>
    public class RorInstruction : InstructionBase
    {
        public RorInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("ROR", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInAccumulatorMode()
        {
            var result = DoRol(Core.Registers.Accumulator);

            // 1 cycle
            Core.Registers.Accumulator = result;
            Core.YieldCycle();
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 3 cycle
            LoadAndDoRol(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 3 cycle
            LoadAndDoRol(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 3 cycle
            LoadAndDoRol(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 6.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 3 cycles
            var address = ReadAddressInAbsoluteXMode(true);

            // 3 cycle
            LoadAndDoRol(address);
        }

        /// <summary>
        /// Cycles: 3.
        /// </summary>
        private void LoadAndDoRol(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            var result = DoRol(number);

            // 1 cycle
            Core.YieldCycle();

            // 1 cycle
            Core.Bus.Write(address, result);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private byte DoRol(byte number)
        {
            var result = (byte)(number >> 1);
            if (Core.Registers.Flags.HasFlag(StatusFlags.Carry))
            {
                result |= 1 << 7;
            }

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((result >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            var carryFlag = (number & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Carry, carryFlag);

            return result;
        }
    }
}
