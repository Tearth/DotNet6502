namespace CPU.InstructionDecode.Instructions
{
    public class BrkInstruction : InstructionBase
    {
        public BrkInstruction(ushort opCode, AddressingMode addressingMode) : base("BRK", opCode, addressingMode)
        {

        }

        protected override void ExecuteInImplicitMode()
        {
            // TODO: add implementation
        }
    }
}
