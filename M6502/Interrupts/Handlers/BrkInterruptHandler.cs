namespace M6502.Interrupts.Handlers
{
    public class BrkInterruptHandler : InterruptHandlerBase
    {
        public BrkInterruptHandler(M6502Core core) : base(core)
        {

        }

        public override void Execute()
        {
            // Internal operations (1 cycle)
            Core.YieldCycle();

            // Store registers onto stack (3 cycles)
            PreExecute(false, true);

            // Read BRK vector (2 cycles)
            var vector = (ushort)(Core.Bus.Read(0xFFFE) | (Core.Bus.Read(0xFFFF) << 8));
            Core.Registers.ProgramCounter = vector;
        }
    }
}