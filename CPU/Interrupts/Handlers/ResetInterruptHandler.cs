namespace CPU.Interrupts.Handlers
{
    public class ResetInterruptHandler : InterruptHandlerBase
    {
        public ResetInterruptHandler(Mos6502Core core) : base(core)
        {

        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
