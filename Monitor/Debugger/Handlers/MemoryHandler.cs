using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Responses;

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
                    ViewModel.Code = memoryPacket.Memory;
                    break;
                }

                // Memory
                case 2:
                {
                    ViewModel.Memory = memoryPacket.Memory;
                    break;
                }
            }

            return null;
        }
    }
}