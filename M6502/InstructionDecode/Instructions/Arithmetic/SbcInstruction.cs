using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// ADd with Carry
    /// </summary>
    public class SbcInstruction : InstructionBase
    {
        public SbcInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("SBC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImmediateMode()
        {
            // 1 cycle
            LoadAndDoSub(Core.Registers.ProgramCounter);
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
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 1 cycle
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 1 cycle
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteXMode();

            // 1 cycle
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 3 + 1B.
        /// </summary>
        protected override void ExecuteInAbsoluteYMode()
        {
            // 2 cycles + 1 if page boundary crossed
            var address = ReadAddressInAbsoluteYMode();

            // 1 cycle
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInIndexedIndirectMode()
        {
            // 4 cycles
            var address = ReadAddressInIndexedIndirectMode();

            // 1 cycle
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4 + 1B.
        /// </summary>
        protected override void ExecuteInIndirectIndexedMode()
        {
            // 3 cycles + 1 if page boundary crossed
            var address = ReadAddressInIndirectIndexedMode();

            // 1 cycle
            LoadAndDoSub(address);
        }

        /// <summary>
        /// Cycles: 1.
        /// </summary>
        private void LoadAndDoSub(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            DoSub(number);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private void DoSub(byte number)
        {
            var a = Core.Registers.Accumulator;
            var c = (byte)(Core.Registers.Flags & StatusFlags.Carry);
            uint result;

            if ((Core.Registers.Flags & StatusFlags.DecimalMode) != 0)
            {
                var aLowNibble = a & 0x0F;
                var aHighNibble = (a & 0xF0) >> 4;
                var numberLowNibble = 9 - (number & 0x0F);
                var numberHighNibble = 9 - ((number & 0xF0) >> 4);

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
                }

                result = (uint)((highNibble << 4) | lowNibble);
            }
            else
            {
                number ^= 0xFF;
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
