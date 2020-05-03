namespace M6502
{
    public class M6502Emulator
    {
        public M6502Core Core { get; set; }

        public M6502Emulator(uint frequency)
        {
            Core = new M6502Core(frequency);
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
