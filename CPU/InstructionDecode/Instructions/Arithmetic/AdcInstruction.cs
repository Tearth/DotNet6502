using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
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
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            LoadAndDoAdd(Core.Registers.ProgramCounter);
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
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteXMode();

            // 1 cycle
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteYMode();

            // 1 cycle
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndexedIndirectMode();

            // 1 cycle
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4 + 1B.
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            // 3 cycles + 1 if page boundary crossed
            var address = ReadAddressInIndirectIndexedMode();

            // 1 cycle
            LoadAndDoAdd(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void LoadAndDoAdd(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            DoAdd(number);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private void DoAdd(byte number)
        {
            var a = Core.Registers.Accumulator;
            var c = Core.Registers.Flags.HasFlag(StatusFlags.Carry) ? 1 : 0;
            uint result;

            if (Core.Registers.Flags.HasFlag(StatusFlags.DecimalMode))
            {
                var aLowNibble = a & 0x0F;
                var aHighNibble = (a & 0xF0) >> 4;
                var numberLowNibble = number & 0x0F;
                var numberHighNibble = (number & 0xF0) >> 4;
                var carryNibble = 0;

                var lowNibble = aLowNibble + numberLowNibble + c;
                if (lowNibble > 9)
                {
                    lowNibble += 6;
                    lowNibble &= 0x0F;
                    aHighNibble++;
                }

                var highNibble = aHighNibble + numberHighNibble;
                if (highNibble > 9)
                {
                    highNibble += 6;
                    highNibble &= 0x0F;
                    carryNibble = 1;
                }

                result = (uint)((carryNibble << 8) | (highNibble << 4) | lowNibble);
            }
            else
            {
                result = (uint)(a + number + c);
            }

            Core.Registers.Accumulator = (byte)result;

            var zeroFlag = (byte)result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((result >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            var carryFlag = result > byte.MaxValue;
            Core.Registers.ChangeFlag(StatusFlags.Carry, carryFlag);

            var overflowFlag = ((a ^ (byte)result) & (number ^ (byte)result) & 0x80) != 0;
            Core.Registers.ChangeFlag(StatusFlags.Overflow, overflowFlag);
        }
    }
}
