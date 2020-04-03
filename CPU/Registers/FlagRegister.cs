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

        public FlagRegister()
        {
            IrqDisable = true;
        }
    }
}
