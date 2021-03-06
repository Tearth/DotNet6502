﻿using System;
using System.Diagnostics;
using System.Threading;
using M6502.InstructionDecode;
using M6502.Interrupts;
using M6502.IO;
using M6502.Registers;

namespace M6502
{
    public class M6502Core
    {
        public PinsState Pins { get; set;  }
        public Bus Bus { get; }
        public RegistersState Registers { get; }
        public ulong Cycles { get; private set; }
        public bool YieldingCycle { get; private set; }

        private readonly InstructionDecoder _instructionDecoder;
        private readonly InterruptsLogic _interruptsLogic;

        private readonly uint _frequency;
        private readonly ulong _ticksPerCycle;
        private Stopwatch _stopwatch;

        public M6502Core(uint frequency)
        {
            Pins = new PinsState();
            Bus = new Bus(this);
            Registers = new RegistersState();

            _instructionDecoder = new InstructionDecoder(this);
            _interruptsLogic = new InterruptsLogic(this);
            _frequency = frequency;

            if (frequency != 0)
            {
                _ticksPerCycle = TimeSpan.TicksPerSecond / (ulong)_frequency;
            }
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
            _stopwatch = Stopwatch.StartNew();
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

            if (!Pins.Ready)
            {
                _stopwatch.Stop();
                while (!Pins.Ready)
                {
                    Thread.Sleep(1);
                }
                _stopwatch.Start();
            }

            if (_frequency != 0)
            {
                var ticksToWait = Cycles * _ticksPerCycle;
                while ((ulong)_stopwatch.ElapsedTicks < ticksToWait) ;
            }

            Cycles++;
            YieldingCycle = false;
        }

        public void RequestBrk()
        {
            _interruptsLogic.RequestBrk();
        }
    }
}
