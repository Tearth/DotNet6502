using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class RegistersRequestPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public RegistersRequestPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var requestPacket = new RegistersRequestPacket();
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}