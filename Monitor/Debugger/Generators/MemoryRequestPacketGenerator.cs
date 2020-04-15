using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class MemoryRequestPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public MemoryRequestPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate(ushort address, ushort requestedLength, byte tag)
        {
            var requestPacket = new MemoryRequestPacket(address, requestedLength, tag);
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}