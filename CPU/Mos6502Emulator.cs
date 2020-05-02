namespace CPU
{
    public class Mos6502Emulator
    {
        public Mos6502Core Core { get; set; }

        public Mos6502Emulator(uint frequency)
        {
            Core = new Mos6502Core(frequency);
        }

        public void PowerUp()
        {
            Core.SetPowerState(true);
        }

        public void PowerDown()
        {
            Core.SetPowerState(true);
        }

        public void SetRdyState(bool state)
        {
            Core.Pins.Ready = state;
        }

        public void Reset()
        {
            Core.Reset();
        }

        public void Run()
        {
            Core.Run();
        }
    }
}
