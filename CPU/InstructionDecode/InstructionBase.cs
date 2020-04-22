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

        /// <summary>
        /// Pure instruction, no arguments.
        /// </summary>
        protected virtual void ExecuteInImplicitMode()
        {
            ThrowNotImplementedException("Implicit");
        }

        /// <summary>
        /// Argument is stored in the accumulator register.
        /// </summary>
        protected virtual void ExecuteInAccumulatorMode()
        {
            ThrowNotImplementedException("Accumulator");
        }

        /// <summary>
        /// Argument is stored as constant next to operation code.
        /// </summary>
        protected virtual void ExecuteInImmediateMode()
        {
            ThrowNotImplementedException("Immediate");
        }

        /// <summary>
        /// Arguments length: 1, Cycles: 1.
        /// </summary>
        protected byte ReadAddressInZeroPageMode()
        {
            // 1 cycle
            var address = Core.Bus.Read(Core.Registers.ProgramCounter);
            Core.Registers.ProgramCounter++;

            return address;
        }

        /// <summary>
        /// Argument is stored at the specified 8-bit address (0 page).
        /// </summary>
        protected virtual void ExecuteInZeroPageMode()
        {
            ThrowNotImplementedException("Zero Page");
        }

        /// <summary>
        /// Arguments length: 1, Cycles: 2.
        /// </summary>
        protected byte ReadAddressInZeroPageXMode()
        {
            // 1 cycle
            var zeroPageAddress = ReadAddressInZeroPageMode();

            // 1 cycle
            var address = (byte)(zeroPageAddress + Core.Registers.IndexRegisterX);
            Core.YieldCycle();

            return address;
        }

        /// <summary>
        /// Argument is stored at the specified 8-bit address (0 page) + X register.
        /// </summary>
        protected virtual void ExecuteInZeroPageXMode()
        {
            ThrowNotImplementedException("Zero Page X");
        }


        /// <summary>
        /// Arguments length: 1, Cycles: 2.
        /// </summary>
        protected byte ReadAddressInZeroPageYMode()
        {
            // 1 cycle
            var zeroPageAddress = ReadAddressInZeroPageMode();

            // 1 cycle
            var address = (byte)(zeroPageAddress + Core.Registers.IndexRegisterY);
            Core.YieldCycle();

            return address;
        }

        /// <summary>
        /// Argument is stored at the specified 8-bit address (0 page) + Y register.
        /// </summary>
        protected virtual void ExecuteInZeroPageYMode()
        {
            ThrowNotImplementedException("Zero Page Y");
        }

        /// <summary>
        /// Argument contains offset to apply.
        /// </summary>
        protected virtual void ExecuteInRelativeMode()
        {
            ThrowNotImplementedException("Relative");
        }


        /// <summary>
        /// Arguments length: 2, Cycles: 2.
        /// </summary>
        protected ushort ReadAddressInAbsoluteMode()
        {
            // 1 cycle
            var low = Core.Bus.Read(Core.Registers.ProgramCounter);
            Core.Registers.ProgramCounter++;

            // 1 cycle
            var high = Core.Bus.Read(Core.Registers.ProgramCounter) << 8;
            Core.Registers.ProgramCounter++;

            return (ushort) (high | low);
        }

        /// <summary>
        /// Argument is stored at the specified 16-bit address.
        /// </summary>
        protected virtual void ExecuteInAbsoluteMode()
        {
            ThrowNotImplementedException("Absolute");
        }


        /// <summary>
        /// Arguments length: 2, Cycles: 2 + 1B.
        /// </summary>
        protected ushort ReadAddressInAbsoluteXMode(bool forceBoundaryCheck = false)
        {
            // 2 cycles
            var absoluteAddress = ReadAddressInAbsoluteMode();

            // 1 cycle if page boundary crossed
            var absoluteXAddress = (ushort)(absoluteAddress + Core.Registers.IndexRegisterX);
            if (forceBoundaryCheck || (absoluteAddress & 0xFF00) != (absoluteXAddress & 0xFF00))
            {
                Core.YieldCycle();
            }

            return absoluteXAddress;
        }

        /// <summary>
        /// Argument is stored at the specified 16-bit address + X register.
        /// </summary>
        protected virtual void ExecuteInAbsoluteXMode()
        {
            ThrowNotImplementedException("Absolute X");
        }
        
        /// <summary>
        /// Arguments length: 2, Cycles: 2 + 1B.
        /// </summary>
        protected ushort ReadAddressInAbsoluteYMode(bool forceBoundaryCheck = false)
        {
            // 2 cycles
            var absoluteAddress = ReadAddressInAbsoluteMode();

            // 1 cycle if page boundary crossed
            var absoluteYAddress = (ushort)(absoluteAddress + Core.Registers.IndexRegisterY);
            if (forceBoundaryCheck || (absoluteAddress & 0xFF00) != (absoluteYAddress & 0xFF00))
            {
                Core.YieldCycle();
            }

            return absoluteYAddress;
        }

        /// <summary>
        /// Argument is stored at the specified 16-bit address + Y register.
        /// </summary>
        protected virtual void ExecuteInAbsoluteYMode()
        {
            ThrowNotImplementedException("Absolute Y");
        }


        /// <summary>
        /// Arguments length: 2, Cycles: 4.
        /// </summary>
        protected ushort ReadAddressInIndirectMode()
        {
            // 2 cycles
            var indirectAddress = ReadAddressInAbsoluteMode();

            // 1 cycle
            var low = Core.Bus.Read(indirectAddress);
            Core.Registers.ProgramCounter++;

            // 1 cycle
            var high = Core.Bus.Read(indirectAddress) << 8;
            Core.Registers.ProgramCounter++;

            return (ushort)(high | low);
        }

        /// <summary>
        /// Address of the argument is stored at the specified 16-bit address.
        /// </summary>
        protected virtual void ExecuteInIndirectMode()
        {
            ThrowNotImplementedException("Indirect");
        }
        
        /// <summary>
        /// Arguments length: 1, Cycles: 4.
        /// </summary>
        protected ushort ReadAddressInIndexedIndirectMode()
        {
            // 1 cycle
            var tableAddress = ReadAddressInZeroPageMode();

            // 1 cycle
            var indirectAddress = (byte)(tableAddress + Core.Registers.IndexRegisterX);
            Core.YieldCycle();

            // 1 cycle
            var low = Core.Bus.Read(indirectAddress);
            Core.Registers.ProgramCounter++;

            // 1 cycle
            var high = Core.Bus.Read(indirectAddress) << 8;
            Core.Registers.ProgramCounter++;

            return (ushort)(high | low);
        }

        /// <summary>
        /// Address of the argument is stored in the table (0 page) at X register index.
        /// </summary>
        protected virtual void ExecuteInIndexedIndirectMode()
        {
            ThrowNotImplementedException("Indexed Indirect");
        }


        /// <summary>
        /// Arguments length: 1, Cycles: 3 + 1B.
        /// </summary>
        protected ushort ReadAddressInIndirectIndexedMode(bool forceBoundaryCheck = false)
        {
            // 1 cycle
            var indirectAddress = ReadAddressInZeroPageMode();

            // 1 cycle
            var low = Core.Bus.Read(indirectAddress);
            Core.Registers.ProgramCounter++;

            // 1 cycle
            var high = Core.Bus.Read(indirectAddress) << 8;
            Core.Registers.ProgramCounter++;

            var realAddress = (ushort)(high | low);

            // 1 cycle if page boundary crossed
            var realYAddress = (ushort)(realAddress + Core.Registers.IndexRegisterY);
            if (forceBoundaryCheck || (realAddress & 0xFF00) != (realYAddress & 0xFF00))
            {
                Core.YieldCycle();
            }

            return realYAddress;
        }

        /// <summary>
        /// Address of the argument is stored at the specified address (0 page) + Y register index.
        /// </summary>
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
