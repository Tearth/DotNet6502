using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class CyclesRequestPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public CyclesRequestPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var requestPacket = new CyclesRequestPacket();
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}