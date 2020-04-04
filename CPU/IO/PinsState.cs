namespace CPU.IO
{
    public class PinsState
    {
        public bool Vcc { get; set; }
        public bool Res { get; set; }
        public bool Rdy { get; set; }
        public bool Irq { get; set; }
        public bool Nmi { get; set; }
        public bool Sync { get; set; }
        public bool Rw { get; set; }
        public ushort A { get; set; }
        public byte D { get; set; }
    }
}
