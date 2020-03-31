using System;
using System.Collections.Generic;

namespace Emulator.InstructionDecode
{
    public abstract class InstructionBase
    {
        public string Name { get; }
        public ushort OpCode { get; }
        public AddressingMode AddressingMode { get; }

        private readonly Dictionary<AddressingMode, InstructionExecutor> _executors;

        protected InstructionBase(string name, ushort opCode, AddressingMode addressingMode)
        {
            Name = name;
            OpCode = opCode;
            AddressingMode = addressingMode;

            _executors = new Dictionary<AddressingMode, InstructionExecutor>
            {
                { AddressingMode.Implicit, ExecuteInImplicitMode },
                { AddressingMode.Accumulator, ExecuteInAccumulatorMode },
                { AddressingMode.Immediate, ExecuteInImmediateMode },
                { AddressingMode.ZeroPage, ExecuteInZeroPageMode },
                { AddressingMode.ZeroPageX, ExecuteInZeroPageXMode },
                { AddressingMode.ZeroPageY, ExecuteInZeroPageYMode },
                { AddressingMode.Relative, ExecuteInRelativeMode },
                { AddressingMode.Absolute, ExecuteInAbsoluteMode },
                { AddressingMode.AbsoluteX, ExecuteInAbsoluteXMode },
                { AddressingMode.AbsoluteY, ExecuteInAbsoluteYMode },
                { AddressingMode.Indirect, ExecuteInIndirectMode },
                { AddressingMode.IndexedIndirect, ExecuteInIndexedIndirectMode },
                { AddressingMode.IndirectIndexed, ExecuteInIndirectIndexedMode },
            };
        }

        public void Execute()
        {
            _executors[AddressingMode]();
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
            throw new NotImplementedException($"{requestedAddressingMode} mode for {Name} {OpCode:X} not supported.");
        }
    }
}
