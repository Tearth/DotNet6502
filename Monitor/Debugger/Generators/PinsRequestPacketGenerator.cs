using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class PinsRequestPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public PinsRequestPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var requestPacket = new PinsRequestPacket();
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}
