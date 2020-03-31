namespace Emulator.InstructionDecode.Instructions
{
    public class BrkInstruction : InstructionBase
    {
        public BrkInstruction(string name, ushort opCode, AddressingMode addressingMode) : base(name, opCode, addressingMode)
        {

        }

        protected override void ExecuteInImplicitMode()
        {
            // TODO: add implementation
        }
    }
}
