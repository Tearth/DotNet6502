using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// test BITs
    /// </summary>
    public class BitInstruction : InstructionBase
    {
        public BitInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("BIT", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 3.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 2 cycle
            LoadAndDoTest(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 2 cycle
            LoadAndDoTest(address);
        }

        /// <summary>
        /// Cycles: 2.
        /// </summary>
        private void LoadAndDoTest(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            DoTest(number);

            // 1 cycle
            Core.YieldCycle();
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private void DoTest(byte number)
        {
            var result = (byte)(number & Core.Registers.Accumulator);

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var overflowFlag = (number & (1 << 6)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Overflow, overflowFlag);

            var signFlag = (number & (1 << 7)) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);
        }
    }
}
