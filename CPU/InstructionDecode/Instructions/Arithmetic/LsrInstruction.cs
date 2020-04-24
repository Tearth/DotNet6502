using CPU.Registers;

namespace CPU.InstructionDecode.Instructions.Arithmetic
{
    /// <summary>
    /// ADd with Carry
    /// </summary>
    public class LsrInstruction : InstructionBase
    {
        public LsrInstruction(ushort opCode, AddressingMode addressingMode, Mos6502Core core) : base("LSR", opCode, addressingMode, core)
        {

        }

        /// <summary>
        /// Length: 1, Cycles: 1F + 1.
        /// </summary>
        protected override void ExecuteInAccumulatorMode()
        {
            var result = DoLsr(Core.Registers.Accumulator);

            // 1 cycle
            Core.Registers.Accumulator = result;
            Core.YieldCycle();
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 4.
        /// </summary>
        protected override void ExecuteInZeroPageMode()
        {
            // 1 cycle
            var address = ReadAddressInZeroPageMode();

            // 3 cycle
            LoadAndDoAsl(address);
        }

        /// <summary>
        /// Length: 2, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInZeroPageXMode()
        {
            // 2 cycles
            var address = ReadAddressInZeroPageXMode();

            // 3 cycle
            LoadAndDoAsl(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 5.
        /// </summary>
        protected override void ExecuteInAbsoluteMode()
        {
            // 2 cycles
            var address = ReadAddressInAbsoluteMode();

            // 3 cycle
            LoadAndDoAsl(address);
        }

        /// <summary>
        /// Length: 3, Cycles: 1F + 6.
        /// </summary>
        protected override void ExecuteInAbsoluteXMode()
        {
            // 3 cycles
            var address = ReadAddressInAbsoluteXMode(true);

            // 3 cycle
            LoadAndDoAsl(address);
        }

        /// <summary>
        /// Cycles: 3.
        /// </summary>
        private void LoadAndDoAsl(ushort address)
        {
            // 1 cycle
            var number = Core.Bus.Read(address);

            var result = DoLsr(number);

            // 1 cycle
            Core.YieldCycle();

            // 1 cycle
            Core.Bus.Write(address, result);
        }

        /// <summary>
        /// Cycles: 0.
        /// </summary>
        private byte DoLsr(byte number)
        {
            var result = (byte)(number >> 1);

            var zeroFlag = result == 0;
            Core.Registers.ChangeFlag(StatusFlags.Zero, zeroFlag);

            var signFlag = ((result >> 7) & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Sign, signFlag);

            var carryFlag = (number & 1) == 1;
            Core.Registers.ChangeFlag(StatusFlags.Carry, carryFlag);

            return result;
        }
    }
}
