using System;
using System.Collections.Generic;
using CPU.InstructionDecode.Instructions;

namespace CPU.InstructionDecode
{
    public class InstructionDecoder
    {
        private readonly Mos6502Core _core;
        private readonly Dictionary<ushort, InstructionBase> _instructions;

        public InstructionDecoder(Mos6502Core core)
        {
            _instructions = new Dictionary<ushort, InstructionBase>();
            _core = core;

            AddInstruction(new AdcInstruction(0x69, AddressingMode.Immediate, _core));
            AddInstruction(new AdcInstruction(0x65, AddressingMode.ZeroPage, _core));
            AddInstruction(new AdcInstruction(0x75, AddressingMode.ZeroPageX, _core));
            AddInstruction(new AdcInstruction(0x6D, AddressingMode.Absolute, _core));
            AddInstruction(new AdcInstruction(0x7D, AddressingMode.AbsoluteX, _core));
            AddInstruction(new AdcInstruction(0x79, AddressingMode.AbsoluteY, _core));
            AddInstruction(new AdcInstruction(0x61, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new AdcInstruction(0x71, AddressingMode.IndexedIndirect, _core));
        }

        public void DecodeAndExecute()
        {
            _core.Pins.Sync = true;
            var opCode = _core.Bus.Read(_core.Registers.ProgramCounter);
            var instruction = _instructions[opCode];
            _core.Pins.Sync = false;

            instruction.Execute();
        }

        private void AddInstruction(InstructionBase instruction)
        {
            if (_instructions.ContainsKey(instruction.OpCode))
            {
                throw new ArgumentException($"Instruction {instruction.Name} (${instruction.OpCode}) already exists.", nameof(instruction));
            }

            _instructions[instruction.OpCode] = instruction;
        }
    }
}
