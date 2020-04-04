using System;
using CPU.InstructionDecode;
using CPU.Interrupts;
using CPU.IO;
using CPU.Registers;

namespace CPU
{
    public class Mos6502Core
    {
        public readonly PinsState Pins;
        public readonly Bus Bus;
        public readonly RegistersState Registers;

        private readonly InstructionDecoder _instructionDecoder;
        private readonly InterruptsLogic _interruptsLogic;

        private uint _frequency;
        private ulong _ticksPerCycle;
        private ulong _cyclesCounter;
        private DateTime _startTime;

        public Mos6502Core(uint frequency)
        {
            Pins = new PinsState();
            Bus = new Bus(this);
            Registers = new RegistersState();

            _instructionDecoder = new InstructionDecoder(this);
            _interruptsLogic = new InterruptsLogic(this);

            _frequency = frequency;
            _ticksPerCycle = TimeSpan.TicksPerSecond / (ulong)_frequency;
        }

        public void SetPowerState(bool state)
        {
            Pins.Vcc = state;
        }

        public void Reset()
        {
            Pins.Reset = false;
            Pins.Rdy = true;
            _interruptsLogic.Process();
            Pins.Reset = true;
        }

        public void Run()
        {
            _startTime = DateTime.Now;
            while (Pins.Vcc)
            {
                if (_interruptsLogic.Process())
                {
                    continue;
                }

                if (Pins.Reset)
                {
                    _instructionDecoder.DecodeAndExecute();
                }
            }
        }

        public void YieldCycle()
        {
            if (!Pins.Rdy)
            {
                while (!Pins.Rdy) ;
                _startTime = DateTime.Now;
            }

            var ticksToWait = _cyclesCounter * _ticksPerCycle;
            while ((ulong)(DateTime.Now - _startTime).Ticks < ticksToWait) ;

            _cyclesCounter++;
        }
    }
}
