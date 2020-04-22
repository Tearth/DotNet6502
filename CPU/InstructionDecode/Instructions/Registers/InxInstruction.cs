using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Registers
{
    /// <summary>
    /// INcrement X
    /// </summary>
    public class InxInstruction : InstructionBase
    {
        public InxInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("INX", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.IndexRegisterX++;

            var zeroFlag = Core.Registers.IndexRegisterX == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = (Core.Registers.IndexRegisterX & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            Core.YieldCycle();
        }
    }
}