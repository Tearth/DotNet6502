using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Status
{
    /// <summary>
    /// SEt Carry
    /// </summary>
    public class SecInstruction : InstructionBase
    {
        public SecInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("SEC", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInImplicitMode()
        {
            // 1 cycle
            Core.Registers.ChangeFlag(StatusFlags.Carry, true);
            Core.YieldCycle();
        }
    }
}