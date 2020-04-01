using Emulator.Helpers.Extensions;
using Emulator.InstructionDecode;
using Emulator.InstructionDecode.Instructions;
using Emulator.Registers;

namespace Emulator
{
    public class Mos6502Core
    {
        private InstructionDecoder _instructionDecoder;

        public void Run()
        {
            _instructionDecoder = new InstructionDecoder();
        }
    }
}
