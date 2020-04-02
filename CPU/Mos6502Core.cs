using System;
using System.Diagnostics;
using CPU.InstructionDecode;
using CPU.IO;
using CPU.Registers;

namespace CPU
{
    public class Mos6502Core
    {
        private readonly RegistersState _registersState;
        private readonly InstructionDecoder _instructionDecoder;
        private readonly Bus _bus;

        private uint _frequency;
        private double _timePerCycle;
        private ulong _cyclesCounter;
        private ulong _cyclesSinceLastOperation;
        private readonly Stopwatch _cyclesTimer;

        public Mos6502Core(uint frequency)
        {
            _registersState = new RegistersState();
            _instructionDecoder = new InstructionDecoder();
            _bus = new Bus();

            _frequency = frequency;
            _timePerCycle = 1000.0 / frequency;
            _cyclesTimer = new Stopwatch();
        }

        public void Run()
        {
            _cyclesTimer.Start();
            while (WaitForAvailableCycle())
            {
                _cyclesTimer.Restart();
                _cyclesSinceLastOperation = 1;
                _cyclesCounter += _cyclesSinceLastOperation;
            }
        }

        public void AttachDeviceToBus(IDevice device)
        {
            _bus.AttachDevice(device);
        }

        private bool WaitForAvailableCycle()
        {
            var expectedTime = _cyclesSinceLastOperation * _timePerCycle;
            while (_cyclesTimer.Elapsed.TotalMilliseconds < expectedTime);

            return true;
        }
    }
}
