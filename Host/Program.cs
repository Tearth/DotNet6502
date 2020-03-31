using Emulator;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            new Mos6502Core().Run();
        }
    }
}
