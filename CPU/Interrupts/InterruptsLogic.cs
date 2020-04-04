using System.Collections.Generic;
using CPU.Interrupts.Handlers;

namespace CPU.Interrupts
{
    public class InterruptsLogic
    {
        public ushort IrqBrkVector => (ushort)(_core.Bus.Read(0xFFFE) | (_core.Bus.Read(0xFFFF) << 8));
        public ushort NmiVector => (ushort)(_core.Bus.Read(0xFFFA) | (_core.Bus.Read(0xFFFB) << 8));
        public ushort ResetVector => (ushort)(_core.Bus.Read(0xFFFC) | (_core.Bus.Read(0xFFFD) << 8));

        private Mos6502Core _core;
        private readonly Dictionary<string, InterruptHandlerBase> _handlers;

        private bool _oldNmiPinState;
        private bool _oldIrqPinState;

        public InterruptsLogic(Mos6502Core core)
        {
            _core = core;
            _handlers = new Dictionary<string, InterruptHandlerBase>
            {
                { "RESET", new ResetInterruptHandler(_core) }
            };
        }

        public bool Process()
        {
            if (!_core.Pins.Reset)
            {
                _handlers["RESET"].Execute();
                return true;
            }

            _oldIrqPinState = _core.Pins.Irq;
            _oldNmiPinState = _core.Pins.Nmi;

            return false;
        }
    }
}
