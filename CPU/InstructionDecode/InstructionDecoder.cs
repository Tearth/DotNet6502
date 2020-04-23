using System;
using System.Collections.Generic;
using CPU.InstructionDecode.Instructions;
using CPU.InstructionDecode.Instructions.Arithmetic;
using CPU.InstructionDecode.Instructions.Branch;
using CPU.InstructionDecode.Instructions.Flow;
using CPU.InstructionDecode.Instructions.Registers;
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

            // AND
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

            // Exclusive OR
            AddInstruction(new EorInstruction(0x49, AddressingMode.Immediate, _core));
            AddInstruction(new EorInstruction(0x45, AddressingMode.ZeroPage, _core));
            AddInstruction(new EorInstruction(0x55, AddressingMode.ZeroPageX, _core));
            AddInstruction(new EorInstruction(0x4D, AddressingMode.Absolute, _core));
            AddInstruction(new EorInstruction(0x5D, AddressingMode.AbsoluteX, _core));
            AddInstruction(new EorInstruction(0x59, AddressingMode.AbsoluteY, _core));
            AddInstruction(new EorInstruction(0x41, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new EorInstruction(0x51, AddressingMode.IndirectIndexed, _core));

            // OR
            AddInstruction(new OraInstruction(0x09, AddressingMode.Immediate, _core));
            AddInstruction(new OraInstruction(0x05, AddressingMode.ZeroPage, _core));
            AddInstruction(new OraInstruction(0x15, AddressingMode.ZeroPageX, _core));
            AddInstruction(new OraInstruction(0x0D, AddressingMode.Absolute, _core));
            AddInstruction(new OraInstruction(0x1D, AddressingMode.AbsoluteX, _core));
            AddInstruction(new OraInstruction(0x19, AddressingMode.AbsoluteY, _core));
            AddInstruction(new OraInstruction(0x01, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new OraInstruction(0x11, AddressingMode.IndirectIndexed, _core));

            // Rotate left
            AddInstruction(new RolInstruction(0x2A, AddressingMode.Accumulator, _core));
            AddInstruction(new RolInstruction(0x26, AddressingMode.ZeroPage, _core));
            AddInstruction(new RolInstruction(0x36, AddressingMode.ZeroPageX, _core));
            AddInstruction(new RolInstruction(0x2E, AddressingMode.Absolute, _core));
            AddInstruction(new RolInstruction(0x3E, AddressingMode.AbsoluteX, _core));

            // Rotate right
            AddInstruction(new RorInstruction(0x6A, AddressingMode.Accumulator, _core));
            AddInstruction(new RorInstruction(0x66, AddressingMode.ZeroPage, _core));
            AddInstruction(new RorInstruction(0x76, AddressingMode.ZeroPageX, _core));
            AddInstruction(new RorInstruction(0x6E, AddressingMode.Absolute, _core));
            AddInstruction(new RorInstruction(0x7E, AddressingMode.AbsoluteX, _core));

            // Incrementation
            AddInstruction(new IncInstruction(0xE6, AddressingMode.ZeroPage, _core));
            AddInstruction(new IncInstruction(0xF6, AddressingMode.ZeroPageX, _core));
            AddInstruction(new IncInstruction(0xEE, AddressingMode.Absolute, _core));
            AddInstruction(new IncInstruction(0xFE, AddressingMode.AbsoluteX, _core));

            // Decrementation
            AddInstruction(new DecInstruction(0xC6, AddressingMode.ZeroPage, _core));
            AddInstruction(new DecInstruction(0xD6, AddressingMode.ZeroPageX, _core));
            AddInstruction(new DecInstruction(0xCE, AddressingMode.Absolute, _core));
            AddInstruction(new DecInstruction(0xDE, AddressingMode.AbsoluteX, _core));

            // Compare instructions
            AddInstruction(new CmpInstruction(0xC9, AddressingMode.Immediate, _core));
            AddInstruction(new CmpInstruction(0xC5, AddressingMode.ZeroPage, _core));
            AddInstruction(new CmpInstruction(0xD5, AddressingMode.ZeroPageX, _core));
            AddInstruction(new CmpInstruction(0xCD, AddressingMode.Absolute, _core));
            AddInstruction(new CmpInstruction(0xDD, AddressingMode.AbsoluteX, _core));
            AddInstruction(new CmpInstruction(0xD9, AddressingMode.AbsoluteY, _core));
            AddInstruction(new CmpInstruction(0xC1, AddressingMode.IndexedIndirect, _core));
            AddInstruction(new CmpInstruction(0xD1, AddressingMode.IndirectIndexed, _core));
            AddInstruction(new CpxInstruction(0xE0, AddressingMode.Immediate, _core));
            AddInstruction(new CpxInstruction(0xE4, AddressingMode.ZeroPage, _core));
            AddInstruction(new CpxInstruction(0xEC, AddressingMode.Absolute, _core));
            AddInstruction(new CpyInstruction(0xC0, AddressingMode.Immediate, _core));
            AddInstruction(new CpyInstruction(0xC4, AddressingMode.ZeroPage, _core));
            AddInstruction(new CpyInstruction(0xCC, AddressingMode.Absolute, _core));

            // Register instructions
            AddInstruction(new TaxInstruction(0xAA, AddressingMode.Implicit, _core));
            AddInstruction(new TxaInstruction(0x8A, AddressingMode.Implicit, _core));
            AddInstruction(new DexInstruction(0xCA, AddressingMode.Implicit, _core));
            AddInstruction(new InxInstruction(0xE8, AddressingMode.Implicit, _core));
            AddInstruction(new TayInstruction(0xA8, AddressingMode.Implicit, _core));
            AddInstruction(new TyaInstruction(0x98, AddressingMode.Implicit, _core));
            AddInstruction(new DeyInstruction(0x88, AddressingMode.Implicit, _core));
            AddInstruction(new InyInstruction(0xC8, AddressingMode.Implicit, _core));

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
            AddInstruction(new StxInstruction(0x86, AddressingMode.ZeroPage, _core));
            AddInstruction(new StxInstruction(0x96, AddressingMode.ZeroPageY, _core));
            AddInstruction(new StxInstruction(0x8E, AddressingMode.Absolute, _core));
            AddInstruction(new StyInstruction(0x84, AddressingMode.ZeroPage, _core));
            AddInstruction(new StyInstruction(0x94, AddressingMode.ZeroPageY, _core));
            AddInstruction(new StyInstruction(0x8C, AddressingMode.Absolute, _core));

            // Branch instructions
            AddInstruction(new BplInstruction(0x10, AddressingMode.Relative, _core));
            AddInstruction(new BmiInstruction(0x30, AddressingMode.Relative, _core));
            AddInstruction(new BvcInstruction(0x50, AddressingMode.Relative, _core));
            AddInstruction(new BvsInstruction(0x70, AddressingMode.Relative, _core));
            AddInstruction(new BccInstruction(0x90, AddressingMode.Relative, _core));
            AddInstruction(new BcsInstruction(0xB0, AddressingMode.Relative, _core));
            AddInstruction(new BneInstruction(0xD0, AddressingMode.Relative, _core));
            AddInstruction(new BeqInstruction(0xF0, AddressingMode.Relative, _core));

            // Flow
            AddInstruction(new JmpInstruction(0x4C, AddressingMode.Absolute, _core));
            AddInstruction(new JmpInstruction(0x6C, AddressingMode.Indirect, _core));
            AddInstruction(new JsrInstruction(0x20, AddressingMode.Absolute, _core));
            AddInstruction(new RtsInstruction(0x60, AddressingMode.Immediate, _core));
            AddInstruction(new RtiInstruction(0x40, AddressingMode.Immediate, _core));
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
