using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class NextCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public NextCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var commandPacket = new NextCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}