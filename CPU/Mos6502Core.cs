using System;
using System.Threading;
using System.Threading.Tasks;
using CPU.InstructionDecode;
using CPU.Interrupts;
using CPU.IO;
using CPU.Registers;

namespace CPU
{
    public class Mos6502Core
    {
        public PinsState Pins { get; set;  }
        public Bus Bus { get; }
        public RegistersState Registers { get; }
        public ulong Cycles { get; private set; }
        public bool YieldingCycle { get; private set; }

        private readonly InstructionDecoder _instructionDecoder;
        private readonly InterruptsLogic _interruptsLogic;

        private uint _frequency;
        private ulong _ticksPerCycle;
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
            YieldingCycle = true;
            if (!Pins.Rdy)
            {
                while (!Pins.Rdy)
                {
                    Thread.Sleep(1);
                }

                _startTime = DateTime.Now;
            }

            var ticksToWait = Cycles * _ticksPerCycle;
            while ((ulong)(DateTime.Now - _startTime).Ticks < ticksToWait) ;

            Cycles++;
            YieldingCycle = false;
        }

        public void RequestBrk()
        {
            _interruptsLogic.RequestBrk();
        }
    }
}
