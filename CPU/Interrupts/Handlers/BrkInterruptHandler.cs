namespace CPU.Interrupts.Handlers
{
    public class BrkInterruptHandler : InterruptHandlerBase
    {
        public BrkInterruptHandler(Mos6502Core core) : base(core)
        {

        }

        public override void Execute()
        {
            // Internal operations (1 cycle)
            _core.YieldCycle();

            // Store registers onto stack (3 cycles)
            PreExecute(false, true);

            // Read BRK vector (2 cycles)
            var vector = (ushort)(_core.Bus.Read(0xFFFE) | (_core.Bus.Read(0xFFFF) << 8));
            _core.Registers.ProgramCounter = vector;
        }
    }
}