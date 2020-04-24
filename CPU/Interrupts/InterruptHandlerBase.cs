using CPU.Registers;

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
            _core.Bus.Write((ushort)(0x100 + _core.Registers.StackPointer), (byte)(_core.Registers.ProgramCounter >> 8), forcedRw);
            _core.Registers.StackPointer--;

            _core.Bus.Write((ushort)(0x100 + _core.Registers.StackPointer), (byte)_core.Registers.ProgramCounter, forcedRw);
            _core.Registers.StackPointer--;

            _core.Bus.Write((ushort)(0x100 + _core.Registers.StackPointer), (byte)_core.Registers.Flags, forcedRw);
            _core.Registers.StackPointer--;

            _core.Registers.ChangeFlag(StatusFlags.IrqDisable, true);
        }

        public abstract void Execute();
    }
}
