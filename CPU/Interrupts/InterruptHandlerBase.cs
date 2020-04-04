namespace CPU.Interrupts
{
    public abstract class InterruptHandlerBase
    {
        protected readonly Mos6502Core _core;

        protected InterruptHandlerBase(Mos6502Core core)
        {
            _core = core;
        }

        public abstract void Execute();
    }
}
