using System;

namespace CPU.Registers
{
    public class FlagRegister
    {
        public bool Carry { get; set; }
        public bool Zero { get; set; }
        public bool IrqDisable { get; set; }
        public bool DecimalMode { get; set; }
        public bool BrkCommand { get; set; }
        public bool Overflow { get; set; }
        public bool Negative { get; set; }

        public byte Value
        {
            get =>
                (byte)((Carry       ? 1 : 0) << 7 |
                       (Zero        ? 1 : 0) << 6 |
                       (IrqDisable  ? 1 : 0) << 5 |
                       (DecimalMode ? 1 : 0) << 4 |
                       (BrkCommand  ? 1 : 0) << 2 |
                       (Overflow    ? 1 : 0) << 1 |
                       (Negative    ? 1 : 0));

            set
            {
                Carry = Convert.ToBoolean((value >> 7) & 1);
                Zero = Convert.ToBoolean((value >> 6) & 1);
                IrqDisable = Convert.ToBoolean((value >> 5) & 1);
                DecimalMode = Convert.ToBoolean((value >> 4) & 1);
                BrkCommand = Convert.ToBoolean((value >> 2) & 1);
                Overflow = Convert.ToBoolean((value >> 1) & 1);
                Negative = Convert.ToBoolean((value & 1));
            }
        }

        public FlagRegister()
        {
            IrqDisable = true;
        }
    }
}
