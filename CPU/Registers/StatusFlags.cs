using System;

namespace CPU.Registers
{
    [Flags]
    public enum StatusFlags : byte
    {
        None = 0,
        Carry = 1,
        Zero = 2,
        IrqDisable = 4,
        DecimalMode = 8,
        Reserved = 16,
        BrkCommand = 32,
        Overflow = 64,
        Negative = 128
    }
}
