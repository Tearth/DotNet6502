using System;
using CPU.InstructionDecode;
using CPU.IO;
using CPU.Registers;

namespace CPU
{
    public class Mos6502Core
    {
        public readonly Bus Bus;
        public readonly RegistersState Registers;

        private readonly InstructionDecoder _instructionDecoder;

        private uint _frequency;
        private ulong _ticksPerCycle;
        private ulong _cyclesCounter;
        private DateTime _startTime;

        public Mos6502Core(uint frequency)
        {
            Bus = new Bus();
            Registers = new RegistersState();
            _instructionDecoder = new InstructionDecoder(this);

            _frequency = frequency;
            _ticksPerCycle = TimeSpan.TicksPerSecond / (ulong)_frequency;
        }

        public void Run()
        {
            _startTime = DateTime.Now;
            while (true)
            {
                _instructionDecoder.DecodeAndExecute();
            }
        }

        public void YieldCycle()
        {
            var ticksToWait = _cyclesCounter * _ticksPerCycle;
            while ((ulong)(DateTime.Now - _startTime).Ticks < ticksToWait) ;

            _cyclesCounter++;
        }
    }
}
