using CPU.Registers;

namespace CPU.Interrupts
{
    public abstract class InterruptHandlerBase
    {
        protected readonly Mos6502Core Core;

        protected InterruptHandlerBase(Mos6502Core core)
        {
            Core = core;
        }

        protected void PreExecute(bool? forcedRw, bool brk)
        {
            var programCounter = Core.Registers.ProgramCounter;
            if (brk)
            {
                programCounter++;
            }

            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), (byte)(programCounter >> 8), forcedRw);
            Core.Registers.StackPointer--;

            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), (byte)programCounter, forcedRw);
            Core.Registers.StackPointer--;

            var statusFlags = (byte) Core.Registers.Flags;
            if (!brk)
            {
                statusFlags = (byte)(statusFlags & ~(byte)StatusFlags.BrkCommand);
            }

            Core.Bus.Write((ushort)(0x100 + Core.Registers.StackPointer), statusFlags, forcedRw);
            Core.Registers.StackPointer--;

            Core.Registers.ChangeFlag(StatusFlags.IrqDisable, true);
        }

        public abstract void Execute();
    }
}
