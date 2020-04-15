using System;
using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Handlers
{
    public class CyclesHandler : PacketHandlerBase
    {
        public CyclesHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var registersPacket = (CyclesPacket)packet;

            ViewModel.Cycles = registersPacket.Cycles;

            return null;
        }
    }
}