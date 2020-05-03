using System.Collections.Generic;
using M6502.Interrupts.Handlers;

namespace M6502.Interrupts
{
    public class InterruptsLogic
    {
        private readonly M6502Core _core;
        private readonly Dictionary<string, InterruptHandlerBase> _handlers;

        private bool _oldResetPinState;
        private bool _oldNmiPinState;
        private bool _oldIrqPinState;
        private bool _requestBrk;

        public InterruptsLogic(M6502Core core)
        {
            _core = core;
            _handlers = new Dictionary<string, InterruptHandlerBase>
            {
                { "RESET", new ResetInterruptHandler(_core) },
                { "BRK", new BrkInterruptHandler(_core) }
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

            if (_requestBrk)
            {
                _handlers["BRK"].Execute();
                _requestBrk = false;
                return true;
            }

            _oldResetPinState = _core.Pins.Reset;
            _oldIrqPinState = _core.Pins.InterruptRequest;
            _oldNmiPinState = _core.Pins.NonMaskableInterrupt;

            return false;
        }

        public void RequestBrk()
        {
            _requestBrk = true;
        }
    }
}
