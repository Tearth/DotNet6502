using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Monitor.Debugger.Handlers
{
    public class CyclesHandler : PacketHandlerBase
    {
        public CyclesHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var cyclesPacket = (CyclesPacket)packet;

            ViewModel.Cycles = cyclesPacket.Cycles;

            return null;
        }
    }
}