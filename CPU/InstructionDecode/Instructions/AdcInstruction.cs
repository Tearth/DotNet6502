namespace CPU.InstructionDecode.Instructions
{
    public class AdcInstruction : InstructionBase
    {
        private readonly Mos6502Core _core;

        public AdcInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("ADC", opCode, addressingMode)
        {
            _core = core;
        }

        /*
        protected override void ExecuteInImmediateMode()
        {
            var number = _core.Bus.Read(_core.Registers.ProgramCounter++);
            AddWithCarry(number);

            return 2;
        }

        protected override void ExecuteInZeroPageMode()
        {
            var address = _core.Bus.Read(_core.Registers.ProgramCounter++);
            var number = _core.Bus.Read(address);
            AddWithCarry(number);

            return 3;
        }

        protected override void ExecuteInZeroPageXMode()
        {
            var zeroPageAddress = _core.Bus.Read(_core.Registers.ProgramCounter++);
            var address = (byte)(zeroPageAddress + _core.Registers.IndexRegisterX);
            var number = _core.Bus.Read(address);
            AddWithCarry(number);

            return 4;
        }

        protected override void ExecuteInAbsoluteMode()
        {
            var address = _core.Bus.ReadTwo(_core.Registers.ProgramCounter);
            var number = _core.Bus.Read(address);
            _core.Registers.ProgramCounter += 2;
            AddWithCarry(number);

            return 4;
        }

        protected override void ExecuteInAbsoluteXMode()
        {
            var absoluteAddress = _core.Bus.ReadTwo(_core.Registers.ProgramCounter);
            var absoluteXAddress = (ushort)(absoluteAddress + _core.Registers.IndexRegisterX);

            var number = _core.Bus.Read(absoluteXAddress);
            _core.Registers.ProgramCounter += 2;
            AddWithCarry(number);

            if ((absoluteAddress & 0xFF00) != (absoluteXAddress & 0xFF00))
            {
                return 5;
            }

            return 4;
        }

        protected override void ExecuteInAbsoluteYMode()
        {
            var absoluteAddress = _core.Bus.ReadTwo(_core.Registers.ProgramCounter);
            var absoluteXAddress = (ushort)(absoluteAddress + _core.Registers.IndexRegisterY);

            var number = _core.Bus.Read(absoluteXAddress);
            _core.Registers.ProgramCounter += 2;
            AddWithCarry(number);

            if ((absoluteAddress & 0xFF00) != (absoluteXAddress & 0xFF00))
            {
                return 5;
            }

            return 4;
        }

        protected override void ExecuteInIndexedIndirectMode()
        {
            return 0;
        }

        protected override void ExecuteInIndirectIndexedMode()
        {
            return 0;
        }
        */
        private void AddWithCarry(byte number)
        {
            var a = _core.Registers.Accumulator;
            var c = _core.Registers.Flags.Carry ? 1 : 0;
            var result = a + number + c;

            _core.Registers.Accumulator = (byte)result;
            _core.Registers.Flags.Zero = result == 0;
            _core.Registers.Flags.Negative = (result & (1 << 7)) == 1;
            _core.Registers.Flags.Carry = result > byte.MaxValue || result < byte.MinValue;
            _core.Registers.Flags.Overflow = ((a ^ (sbyte)result) & (number ^ (sbyte)result) & 0x80) != 0;
        }
    }
}
