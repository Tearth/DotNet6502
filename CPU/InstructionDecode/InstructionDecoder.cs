using System;
using System.Collections.Generic;
using CPU.InstructionDecode.Instructions;

namespace CPU.InstructionDecode
{
    public class InstructionDecoder
    {
        private readonly CycleContext _cycleContext;
        private readonly Dictionary<ushort, InstructionBase> _instructions;

        public InstructionDecoder(CycleContext cycleContext)
        {
            _instructions = new Dictionary<ushort, InstructionBase>();
            _cycleContext = cycleContext;

            AddInstruction(new BrkInstruction(0x00, AddressingMode.Immediate));
        }

        public uint DecodeAndExecute()
        {
            return 0;
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
