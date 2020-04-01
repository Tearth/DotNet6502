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

        protected virtual void ExecuteInZeroPageMode()
        {
            ThrowNotImplementedException("Zero Page");
        }

        protected virtual void ExecuteInZeroPageXMode()
        {
            ThrowNotImplementedException("Zero Page X");
        }

        protected virtual void ExecuteInZeroPageYMode()
        {
            ThrowNotImplementedException("Zero Page Y");
        }

        protected virtual void ExecuteInRelativeMode()
        {
            ThrowNotImplementedException("Relative");
        }

        protected virtual void ExecuteInAbsoluteMode()
        {
            ThrowNotImplementedException("Absolute");
        }

        protected virtual void ExecuteInAbsoluteXMode()
        {
            ThrowNotImplementedException("Absolute X");
        }

        protected virtual void ExecuteInAbsoluteYMode()
        {
            ThrowNotImplementedException("Absolute Y");
        }

        protected virtual void ExecuteInIndirectMode()
        {
            ThrowNotImplementedException("Indirect");
        }

        protected virtual void ExecuteInIndexedIndirectMode()
        {
            ThrowNotImplementedException("Indexed Indirect");
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
