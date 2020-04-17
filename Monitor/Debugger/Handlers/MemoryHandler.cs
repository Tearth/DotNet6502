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

            switch (memoryPacket.Tag)
            {
                // Stack
                case 0:
                {
                    ViewModel.Stack = memoryPacket.Memory;
                    break;
                }

                // Code
                case 1:
                {
                    ViewModel.DisassembledCode = memoryPacket.Memory;
                    break;
                }
            }

            return null;
        }
    }
}