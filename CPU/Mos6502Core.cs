using CPU.Helpers.Extensions;
using CPU.InstructionDecode;
using CPU.InstructionDecode.Instructions;
using CPU.Registers;

namespace CPU
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
