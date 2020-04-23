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
        /// Length: 2, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            throw new NotImplementedException();
        }
    }
}