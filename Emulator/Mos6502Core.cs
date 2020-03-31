using Emulator.InstructionDecode;

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
