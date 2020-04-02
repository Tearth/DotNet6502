using CPU.IO;
using CPU.Registers;

namespace CPU
{
    public class CycleContext
    {
        public Bus Bus { get; set; }
        public RegistersState RegistersState { get; set; }

        public CycleContext(Bus bus, RegistersState registersState)
        {
            Bus = bus;
            RegistersState = registersState;
        }
    }
}
