namespace CPU.Interrupts
{
    public abstract class InterruptHandlerBase
    {
        protected readonly Mos6502Core _core;

        protected InterruptHandlerBase(Mos6502Core core)
        {
            _core = core;
        }

        protected void PreExecute(bool? forcedRw)
        {
            _core.Bus.Write(_core.Registers.StackPointer--, (byte)(_core.Registers.ProgramCounter >> 8), forcedRw);
            _core.Bus.Write(_core.Registers.StackPointer--, (byte)_core.Registers.ProgramCounter, forcedRw);
            _core.Bus.Write(_core.Registers.StackPointer--, _core.Registers.Flags.Value, forcedRw);

            _core.Registers.Flags.IrqDisable = true;
        }

        public abstract void Execute();
    }
}
