using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions
{
    /// <summary>
    /// Transfer Stack ptr to X
    /// </summary>
    public class TsxInstruction : InstructionBase
    {
        public TsxInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("TSX", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.IndexRegisterX = Core.Registers.StackPointer;
            Core.YieldCycle();
        }
    }
}