using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Commands;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class RunUntilLoopCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public RunUntilLoopCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var commandPacket = new RunUntilLoopCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}