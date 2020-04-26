using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class RunToAddressCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public RunToAddressCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate(ushort address)
        {
            var commandPacket = new RunToAddressCommandPacket()
            {
                Address = address
            };
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}