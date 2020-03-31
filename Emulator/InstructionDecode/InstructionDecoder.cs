using System;
using System.Collections.Generic;
using Emulator.InstructionDecode.Instructions;

namespace Emulator.InstructionDecode
{
    public class InstructionDecoder
    {
        private readonly Dictionary<ushort, InstructionBase> _instructions;

        public InstructionDecoder()
        {
            _instructions = new Dictionary<ushort, InstructionBase>();

            AddInstruction(new BrkInstruction("BRK", 0x00, AddressingMode.Immediate));
        }

        private void AddInstruction(InstructionBase instruction)
        {
            if (_instructions.ContainsKey(instruction.OpCode))
            {
                throw new ArgumentException($"Instruction {instruction.Name} (${instruction.OpCode}) already exists.");
            }

            _instructions[instruction.OpCode] = instruction;
        }
    }
}
