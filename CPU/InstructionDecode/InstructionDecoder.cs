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

            AddInstruction(new BrkInstruction(0x00, AddressingMode.Implicit, _cycleContext));
        }

        public uint DecodeAndExecute()
        {
            var opCode = _cycleContext.Bus.Read(_cycleContext.RegistersState.ProgramCounter);
            var instruction = _instructions[opCode];

            return instruction.Execute();
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
