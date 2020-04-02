namespace CPU.InstructionDecode.Instructions
{
    public class BrkInstruction : InstructionBase
    {
        private CycleContext _cycleContext;

        public BrkInstruction(ushort opCode, AddressingMode addressingMode, CycleContext cycleContext) : base("BRK", opCode, addressingMode)
        {
            _cycleContext = cycleContext;
        }

        protected override uint ExecuteInImplicitMode()
        {
            // TODO: add implementation
            return 0;
        }
    }
}
