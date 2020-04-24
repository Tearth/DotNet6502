using System;
using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Flow
{
    /// <summary>
    /// BReaK
    /// </summary>
    public class BrkInstruction : InstructionBase
    {
        public BrkInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("BRK", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 6.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 6 cycles
            Core.RequestBrk();
        }
    }
}