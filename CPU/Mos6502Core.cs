using System;
using System.Diagnostics;
using CPU.InstructionDecode;
using CPU.IO;
using CPU.Registers;

namespace CPU
{
    public class Mos6502Core
    {
        private readonly Bus _bus;
        private readonly RegistersState _registersState;
        private readonly CycleContext _cycleContext;
        private readonly InstructionDecoder _instructionDecoder;

        private uint _frequency;
        private double _timePerCycle;
        private ulong _cyclesCounter;
        private ulong _cyclesSinceLastOperation;
        private readonly Stopwatch _cyclesTimer;

        public Mos6502Core(uint frequency)
        {
            _bus = new Bus();
            _registersState = new RegistersState();
            _cycleContext = new CycleContext(_bus, _registersState);
            _instructionDecoder = new InstructionDecoder(_cycleContext);

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
                _cyclesSinceLastOperation = _instructionDecoder.DecodeAndExecute();
                _cyclesCounter += _cyclesSinceLastOperation;
            }
        }

        public void AttachDeviceToBus(IDevice device)
        {
            _bus.AttachDevice(device);
        }

        public void SetProgramCounter(ushort programCounter)
        {
            _registersState.ProgramCounter = programCounter;
        }

        private bool WaitForAvailableCycle()
        {
            var expectedTime = _cyclesSinceLastOperation * _timePerCycle;
            while (_cyclesTimer.Elapsed.TotalMilliseconds < expectedTime);

            return true;
        }
    }
}
