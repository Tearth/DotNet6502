using System;
using CPU.Helpers.Extensions;

namespace CPU.InstructionDecode
{
    public abstract class InstructionBase
    {
        public string Name { get; }
        public ushort OpCode { get; }
        public AddressingMode AddressingMode { get; }

        private readonly InstructionExecutor _executor;

        protected InstructionBase(string name, ushort opCode, AddressingMode addressingMode)
        {
            Name = name;
            OpCode = opCode;
            AddressingMode = addressingMode;

            _executor = GetExecutor();
        }

        public uint Execute()
        {
            return _executor();
        }

        public override string ToString()
        {
            return $"{Name} (0x{OpCode:X2})";
        }

        protected virtual uint ExecuteInImplicitMode()
        {
            ThrowNotImplementedException("Implicit");
            return 0;
        }

        protected virtual uint ExecuteInAccumulatorMode()
        {
            ThrowNotImplementedException("Accumulator");
            return 0;
        }

        protected virtual uint ExecuteInImmediateMode()
        {
            ThrowNotImplementedException("Immediate");
            return 0;
        }

        protected virtual uint ExecuteInZeroPageMode()
        {
            ThrowNotImplementedException("Zero Page");
            return 0;
        }

        protected virtual uint ExecuteInZeroPageXMode()
        {
            ThrowNotImplementedException("Zero Page X");
            return 0;
        }

        protected virtual uint ExecuteInZeroPageYMode()
        {
            ThrowNotImplementedException("Zero Page Y");
            return 0;
        }

        protected virtual uint ExecuteInRelativeMode()
        {
            ThrowNotImplementedException("Relative");
            return 0;
        }

        protected virtual uint ExecuteInAbsoluteMode()
        {
            ThrowNotImplementedException("Absolute");
            return 0;
        }

        protected virtual uint ExecuteInAbsoluteXMode()
        {
            ThrowNotImplementedException("Absolute X");
            return 0;
        }

        protected virtual uint ExecuteInAbsoluteYMode()
        {
            ThrowNotImplementedException("Absolute Y");
            return 0;
        }

        protected virtual uint ExecuteInIndirectMode()
        {
            ThrowNotImplementedException("Indirect");
            return 0;
        }

        protected virtual uint ExecuteInIndexedIndirectMode()
        {
            ThrowNotImplementedException("Indexed Indirect");
            return 0;
        }

        protected virtual uint ExecuteInIndirectIndexedMode()
        {
            ThrowNotImplementedException("Indirect Indexed");
            return 0;
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
