using System;
using System.Collections.Generic;
using CPU.InstructionDecode.Instructions;

namespace CPU.InstructionDecode
{
    public class InstructionDecoder
    {
        private readonly Dictionary<ushort, InstructionBase> _instructions;

        public InstructionDecoder()
        {
            _instructions = new Dictionary<ushort, InstructionBase>();

            AddInstruction(new BrkInstruction(0x00, AddressingMode.Immediate));
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
