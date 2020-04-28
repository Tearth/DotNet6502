using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Commands;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class ContinueCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public ContinueCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var commandPacket = new ContinueCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}