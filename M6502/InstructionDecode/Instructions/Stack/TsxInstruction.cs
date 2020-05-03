using M6502.Registers;

namespace M6502.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// Transfer Stack ptr to X
    /// </summary>
    public class TsxInstruction : InstructionBase
    {
        public TsxInstruction(byte opCode, AddressingMode addressingMode, M6502Core core) : base("TSX", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.IndexRegisterX = Core.Registers.StackPointer;

            var zeroFlag = Core.Registers.IndexRegisterX == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((Core.Registers.IndexRegisterX >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}