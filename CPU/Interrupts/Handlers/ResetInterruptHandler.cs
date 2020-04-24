namespace CPU.Interrupts.Handlers
{
    public class ResetInterruptHandler : InterruptHandlerBase
    {
        public ResetInterruptHandler(Mos6502Core core) : base(core)
        {

        }

        public override void Execute()
        {
            // Internal operations (2 cycles)
            _core.YieldCycle();
            _core.YieldCycle();

            // Store (faked) registers onto stack (3 cycles)
            PreExecute(true, false);

            // Read RESET vector (2 cycles)
            var vector = (ushort) (_core.Bus.Read(0xFFFC) | (_core.Bus.Read(0xFFFD) << 8));
            _core.Registers.ProgramCounter = vector;
        }
    }
}
