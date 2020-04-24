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

        protected void PreExecute(bool? forcedRw, bool brk)
        {
            var programCounter = _core.Registers.ProgramCounter;
            if (brk)
            {
                programCounter++;
            }

            _core.Bus.Write((ushort)(0x100 + _core.Registers.StackPointer), (byte)(programCounter >> 8), forcedRw);
            _core.Registers.StackPointer--;

            _core.Bus.Write((ushort)(0x100 + _core.Registers.StackPointer), (byte)programCounter, forcedRw);
            _core.Registers.StackPointer--;

            var statusFlags = (byte) _core.Registers.Flags;
            if (!brk)
            {
                statusFlags = (byte)(statusFlags & ~(byte)StatusFlags.BrkCommand);
            }

            _core.Bus.Write((ushort)(0x100 + _core.Registers.StackPointer), statusFlags, forcedRw);
            _core.Registers.StackPointer--;

            _core.Registers.ChangeFlag(StatusFlags.IrqDisable, true);
        }

        public abstract void Execute();
    }
}
