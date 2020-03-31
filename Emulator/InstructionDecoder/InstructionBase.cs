using System;
using System.Collections.Generic;
using Emulator.Instructions;

namespace Emulator.InstructionDecoder
{
    public abstract class InstructionBase
    {
        public ushort OpCode { get; }
        public AddressingMode AddressingMode { get; }

        private readonly Dictionary<AddressingMode, InstructionExecutor> _executors;

        protected InstructionBase(ushort opCode, AddressingMode addressingMode)
        {
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
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInAccumulatorMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInImmediateMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInZeroPageMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInZeroPageXMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInZeroPageYMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInRelativeMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInAbsoluteMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInAbsoluteXMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInAbsoluteYMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInIndirectMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInIndexedIndirectMode()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteInIndirectIndexedMode()
        {
            throw new NotImplementedException();
        }
    }
}
