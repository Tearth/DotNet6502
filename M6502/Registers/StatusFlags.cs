using System;

namespace M6502.Registers
{
    [Flags]
    public enum StatusFlags : byte
    {
        None = 0,

        /// <summary>
        /// [C] Set when a result of the operation exceeded size of the accumulator.
        /// </summary>
        Carry = 1,

        /// <summary>
        /// [Z] Set when a result of the operation is equal to zero.
        /// </summary>
        Zero = 2,

        /// <summary>
        /// [I] Set when interrupts are disabled (don't affect to NMI).
        /// </summary>
        IrqDisable = 4,

        /// <summary>
        /// [D] Set when decimal mode is enabled (values are treated as BCD numbers).
        /// </summary>
        DecimalMode = 8,

        /// <summary>
        /// [B] Set when software interrupt is executed.
        /// </summary>
        BrkCommand = 16,

        /// <summary>
        /// Reserved (should be set).
        /// </summary>
        Reserved = 32,

        /// <summary>
        /// [V] Set when a result of the operation has been overflowed.
        /// </summary>
        Overflow = 64,

        /// <summary>
        /// [S] Set when a result of the operation is negative.
        /// </summary>
        Sign = 128
    }
}
