using System.Collections.Generic;
using CPU.Interrupts.Handlers;

namespace CPU.Interrupts
{
    public class InterruptsLogic
    {
        private Mos6502Core _core;
        private readonly Dictionary<string, InterruptHandlerBase> _handlers;

        private bool _oldResetPinState;
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
            if (_oldResetPinState != _core.Pins.Reset && _core.Pins.Reset)
            {
                _handlers["RESET"].Execute();
                _oldResetPinState = _core.Pins.Reset;
                return true;
            }

            _oldResetPinState = _core.Pins.Reset;
            _oldIrqPinState = _core.Pins.Irq;
            _oldNmiPinState = _core.Pins.Nmi;

            return false;
        }
    }
}
