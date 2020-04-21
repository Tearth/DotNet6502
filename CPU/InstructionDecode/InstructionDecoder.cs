using System;
using System.Collections.Generic;
using CPU.InstructionDecode.Instructions;
using CPU.InstructionDecode.Instructions.Arithmetic;
using CPU.InstructionDecode.Instructions.Stack;
using CPU.InstructionDecode.Instructions.Status;

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

            // Adding
            AddInstruction(new AdcInstruction(0x69, AddressingMode.Immediate, _core));
            AddInstruction(new AdcInstruction(0x65, AddressingMode.ZeroPage, _core));
            AddInstruction(new AdcInstruction(0x75, AddressingMode.ZeroPageX, _core));
            AddInstruction(new AdcInstruction(0x6D, AddressingMode.Absolute, _core));
            AddInstruction(new AdcInstruction(0x7D, AddressingMode.AbsoluteX, _core));
            AddInstruction(new AdcInstruction(0x79, AddressingMode.AbsoluteY, _core));
            AddInstruction(new AdcInstruction(0x61, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new AdcInstruction(0x71, AddressingMode.IndirectIndexed, _core));

            // Subtracting
            AddInstruction(new SbcInstruction(0xE9, AddressingMode.Immediate, _core));
            AddInstruction(new SbcInstruction(0xE5, AddressingMode.ZeroPage, _core));
            AddInstruction(new SbcInstruction(0xF5, AddressingMode.ZeroPageX, _core));
            AddInstruction(new SbcInstruction(0xED, AddressingMode.Absolute, _core));
            AddInstruction(new SbcInstruction(0xFD, AddressingMode.AbsoluteX, _core));
            AddInstruction(new SbcInstruction(0xF9, AddressingMode.AbsoluteY, _core));
            AddInstruction(new SbcInstruction(0xE1, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new SbcInstruction(0xF1, AddressingMode.IndirectIndexed, _core));

            // And
            AddInstruction(new AndInstruction(0x29, AddressingMode.Immediate, _core));
            AddInstruction(new AndInstruction(0x25, AddressingMode.ZeroPage, _core));
            AddInstruction(new AndInstruction(0x35, AddressingMode.ZeroPageX, _core));
            AddInstruction(new AndInstruction(0x2D, AddressingMode.Absolute, _core));
            AddInstruction(new AndInstruction(0x3D, AddressingMode.AbsoluteX, _core));
            AddInstruction(new AndInstruction(0x39, AddressingMode.AbsoluteY, _core));
            AddInstruction(new AndInstruction(0x21, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new AndInstruction(0x31, AddressingMode.IndirectIndexed, _core));

            // Arithmetic shift left
            AddInstruction(new AslInstruction(0x0A, AddressingMode.Accumulator, _core));
            AddInstruction(new AslInstruction(0x06, AddressingMode.ZeroPage, _core));
            AddInstruction(new AslInstruction(0x16, AddressingMode.ZeroPageX, _core));
            AddInstruction(new AslInstruction(0x0E, AddressingMode.Absolute, _core));
            AddInstruction(new AslInstruction(0x1E, AddressingMode.AbsoluteX, _core));

            // Bit test
            AddInstruction(new BitInstruction(0x24, AddressingMode.ZeroPage, _core));
            AddInstruction(new BitInstruction(0x2C, AddressingMode.Absolute, _core));

            // Flag instructions
            AddInstruction(new ClcInstruction(0x18, AddressingMode.Implicit, _core));
            AddInstruction(new CldInstruction(0xD8, AddressingMode.Implicit, _core));
            AddInstruction(new CliInstruction(0x58, AddressingMode.Implicit, _core));
            AddInstruction(new ClvInstruction(0xB8, AddressingMode.Implicit, _core));
            AddInstruction(new SecInstruction(0x38, AddressingMode.Implicit, _core));
            AddInstruction(new SedInstruction(0xF8, AddressingMode.Implicit, _core));
            AddInstruction(new SeiInstruction(0x78, AddressingMode.Implicit, _core));

            // Stack instructions
            AddInstruction(new PhpInstruction(0x08, AddressingMode.Implicit, _core));
            AddInstruction(new PhaInstruction(0x48, AddressingMode.Implicit, _core));
            AddInstruction(new PlaInstruction(0x68, AddressingMode.Implicit, _core));
            AddInstruction(new PlpInstruction(0x28, AddressingMode.Implicit, _core));
            AddInstruction(new TsxInstruction(0xBA, AddressingMode.Implicit, _core));
            AddInstruction(new TxsInstruction(0x9A, AddressingMode.Implicit, _core));
        }

        public void DecodeAndExecute()
        {
            _core.Pins.Sync = true;
            var opCode = _core.Bus.Read(_core.Registers.ProgramCounter);
            _core.Registers.ProgramCounter++;
            _core.Pins.Sync = false;

            _instructions[opCode].Execute();
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
