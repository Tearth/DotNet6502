namespace CPU
{
    public class Mos6502Emulator
    {
        public Mos6502Core Core { get; set; }

        public Mos6502Emulator(uint frequency)
        {
            Core = new Mos6502Core(frequency);
        }

        public void Run()
        {
            Core.Pins.Vcc = true;
            Core.Pins.Rdy = true;
            Core.Pins.Reset = true;
            Core.Run();
        }
    }
}
