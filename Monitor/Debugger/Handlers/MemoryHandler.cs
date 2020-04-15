using System;
using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Handlers
{
    public class MemoryHandler : PacketHandlerBase
    {
        public MemoryHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var memoryPacket = (MemoryPacket)packet;

            // ViewModel.Cycles = registersPacket.Cycles;

            return null;
        }
    }
}