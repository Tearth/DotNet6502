using System;
using CPU.Helpers.Extensions;

namespace CPU.InstructionDecode
{
    public abstract class InstructionBase
    {
        public string Name { get; }
        public ushort OpCode { get; }
        public AddressingMode AddressingMode { get; }
        protected Mos6502Core Core { get; }

        private readonly InstructionExecutor _executor;

        protected InstructionBase(string name, ushort opCode, AddressingMode addressingMode, Mos6502Core core)
        {
            Name = name;
            OpCode = opCode;
            AddressingMode = addressingMode;
            Core = core;

            _executor = GetExecutor();
        }

        public void Execute()
        {
            _executor();
        }

        public override string ToString()
        {
            return $"{Name} (0x{OpCode:X2})";
        }

        protected virtual void ExecuteInImplicitMode()
        {
            ThrowNotImplementedException("Implicit");
        }

        protected virtual void ExecuteInAccumulatorMode()
        {
            ThrowNotImplementedException("Accumulator");
        }

        protected virtual void ExecuteInImmediateMode()
        {
            ThrowNotImplementedException("Immediate");
        }

        /// <summary>
        /// 1 cycles
        /// </summary>
        protected byte ReadAddressInZeroPageMode()
        {
            return Core.Bus.Read(Core.Registers.ProgramCounter++);
        }

        protected virtual void ExecuteInZeroPageMode()
        {
            ThrowNotImplementedException("Zero Page");
        }

        /// <summary>
        /// 2 cycles
        /// </summary>
        protected byte ReadAddressInZeroPageXMode()
        {
            var zeroPageAddress = ReadAddressInZeroPageMode();
            var address = (byte)(zeroPageAddress + Core.Registers.IndexRegisterX);

            Core.YieldCycle();
            return address;
        }

        protected virtual void ExecuteInZeroPageXMode()
        {
            ThrowNotImplementedException("Zero Page X");
        }

        /// <summary>
        /// 2 cycles
        /// </summary>
        protected byte ReadAddressInZeroPageYMode()
        {
            var zeroPageAddress = ReadAddressInZeroPageMode();
            var address = (byte)(zeroPageAddress + Core.Registers.IndexRegisterY);

            Core.YieldCycle();
            return address;
        }

        protected virtual void ExecuteInZeroPageYMode()
        {
            ThrowNotImplementedException("Zero Page Y");
        }

        protected virtual void ExecuteInRelativeMode()
        {
            ThrowNotImplementedException("Relative");
        }

        /// <summary>
        /// 2 cycles
        /// </summary>
        protected ushort ReadAddressInAbsoluteMode()
        {
            var low = Core.Bus.Read(Core.Registers.ProgramCounter);
            var high = Core.Bus.Read(Core.Registers.ProgramCounter) << 8;
            return (ushort) (high | low);
        }

        protected virtual void ExecuteInAbsoluteMode()
        {
            ThrowNotImplementedException("Absolute");
        }

        /// <summary>
        /// 2+ cycles
        /// </summary>
        protected ushort ReadAddressInAbsoluteXMode()
        {
            var absoluteAddress = ReadAddressInAbsoluteMode();
            var absoluteXAddress = (ushort)(absoluteAddress + Core.Registers.IndexRegisterX);

            if ((absoluteAddress & 0xFF00) != (absoluteXAddress & 0xFF00))
            {
                Core.YieldCycle();
            }

            return absoluteXAddress;
        }

        protected virtual void ExecuteInAbsoluteXMode()
        {
            ThrowNotImplementedException("Absolute X");
        }

        /// <summary>
        /// 2+ cycles
        /// </summary>
        protected ushort ReadAddressInAbsoluteYMode()
        {
            var absoluteAddress = ReadAddressInAbsoluteMode();
            var absoluteYAddress = (ushort)(absoluteAddress + Core.Registers.IndexRegisterY);

            if ((absoluteAddress & 0xFF00) != (absoluteYAddress & 0xFF00))
            {
                Core.YieldCycle();
            }

            return absoluteYAddress;
        }

        protected virtual void ExecuteInAbsoluteYMode()
        {
            ThrowNotImplementedException("Absolute Y");
        }

        /// <summary>
        /// 4 cycles
        /// </summary>
        protected ushort ReadAddressInIndirectMode()
        {
            var indirectAddress = ReadAddressInAbsoluteMode();

            var low = Core.Bus.Read(indirectAddress);
            var high = Core.Bus.Read((ushort)(indirectAddress + 2)) << 8;
            var realAddress = (ushort)(high | low);

            return realAddress;
        }

        protected virtual void ExecuteInIndirectMode()
        {
            ThrowNotImplementedException("Indirect");
        }

        /// <summary>
        /// 5 cycles
        /// </summary>
        protected ushort ReadAddressInIndexedIndirectMode()
        {
            var tableAddress = ReadAddressInZeroPageMode();
            var indirectAddress = (byte)(tableAddress + Core.Registers.IndexRegisterX);
            Core.YieldCycle();

            var low = Core.Bus.Read(indirectAddress);
            var high = Core.Bus.Read((ushort)(indirectAddress + 2)) << 8;
            var realAddress = (ushort)(high | low);
            Core.YieldCycle();

            return realAddress;
        }

        protected virtual void ExecuteInIndexedIndirectMode()
        {
            ThrowNotImplementedException("Indexed Indirect");
        }

        /// <summary>
        /// 4+ cycles
        /// </summary>
        protected ushort ReadAddressInIndirectIndexedMode()
        {
            var indirectAddress = ReadAddressInZeroPageMode();

            var low = Core.Bus.Read(indirectAddress);
            var high = Core.Bus.Read((ushort)(indirectAddress + 2)) << 8;
            var realAddress = (ushort)(high | low);

            var realYAddress = (ushort)(realAddress + Core.Registers.IndexRegisterY);
            if ((realAddress & 0xFF00) != (realYAddress & 0xFF00))
            {
                Core.YieldCycle();
            }

            return realYAddress;
        }

        protected virtual void ExecuteInIndirectIndexedMode()
        {
            ThrowNotImplementedException("Indirect Indexed");
        }

        private void ThrowNotImplementedException(string requestedAddressingMode)
        {
            throw new NotImplementedException($"{requestedAddressingMode} mode for {this} instruction is not supported.");
        }

        private InstructionExecutor GetExecutor()
        {
            switch (AddressingMode)
            {
                case AddressingMode.Implicit: return ExecuteInImplicitMode;
                case AddressingMode.Accumulator: return ExecuteInAccumulatorMode;
                case AddressingMode.Immediate: return ExecuteInImmediateMode;
                case AddressingMode.ZeroPage: return ExecuteInZeroPageMode;
                case AddressingMode.ZeroPageX: return ExecuteInZeroPageXMode;
                case AddressingMode.ZeroPageY: return ExecuteInZeroPageYMode;
                case AddressingMode.Relative: return ExecuteInRelativeMode;
                case AddressingMode.Absolute: return ExecuteInAbsoluteMode;
                case AddressingMode.AbsoluteX: return ExecuteInAbsoluteXMode;
                case AddressingMode.AbsoluteY: return ExecuteInAbsoluteYMode;
                case AddressingMode.Indirect: return ExecuteInIndirectMode;
                case AddressingMode.IndexedIndirect: return ExecuteInIndexedIndirectMode;
                case AddressingMode.IndirectIndexed: return ExecuteInIndirectIndexedMode;
            }

            throw new ArgumentOutOfRangeException($"Can't find proper instruction executor for {AddressingMode.ToString().Smash()} in {this} instruction.");
        }
    }
}
