namespace CPU.InstructionDecode.Instructions.Stack
{
    /// <summary>
    /// Transfer X to Stack ptr
    /// </summary>
    public class TxsInstruction : InstructionBase
    {
        public TxsInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("TXS", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.StackPointer = Core.Registers.IndexRegisterX;
            Core.YieldCycle();
        }
    }
}