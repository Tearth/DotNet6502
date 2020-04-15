using System;

namespace CPU.IO
{
    public class PinsState
    {
        /// <summary>
        /// Power supply (5V). Must be set to true if simulation is running.
        /// </summary>
        public bool Vcc { get; set; }

        /// <summary>
        /// Hardware interrupt used to initialize processor. When set to false, hardware is frozen.
        /// When set to true, processor is normally running. Raising-edge starts initialization process.
        /// </summary>
        public bool Reset { get; set; }

        /// <summary>
        /// When set to false, processor is not executing the next instructions. When set to true, processor
        /// is normally running.
        /// </summary>
        public bool Rdy { get; set; }

        /// <summary>
        /// The hardware interrupt request used to initialize interrupt sequence. Works only if IrqDisable flag is set to false.
        /// Input is level-sensitive.
        /// </summary>
        public bool Irq { get; set; }

        /// <summary>
        /// Non-maskable interrupt. Works similar to typical interrupt, but can't be masked by IrqDisable flag and is edge-sensitive.
        /// </summary>
        public bool Nmi { get; set; }

        /// <summary>
        /// True when processor is fetching instruction operation code, otherwise false.
        /// </summary>
        public bool Sync { get; set; }

        /// <summary>
        /// True when the processor is reading data from the bus, and false if the processor is writing data to the bus.
        /// </summary>
        public bool Rw { get; set; }

        /// <summary>
        /// Address bus (A0 - A15).
        /// </summary>
        public ushort A { get; set; }

        /// <summary>
        /// Data bus (D0 - D7).
        /// </summary>
        public byte D { get; set; }
    }
}
