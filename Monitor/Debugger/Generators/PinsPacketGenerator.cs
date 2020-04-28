using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;
using Protocol.Packets.Responses;

namespace Monitor.Debugger.Generators
{
    public class PinsPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public PinsPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var pinsPacket = new PinsPacket
            {
                AddressBus =
                    (ushort)
                    (
                        ((_viewModel.Pins.A0 ? 1 : 0) << 0) |
                        ((_viewModel.Pins.A1 ? 1 : 0) << 1) |
                        ((_viewModel.Pins.A2 ? 1 : 0) << 2) |
                        ((_viewModel.Pins.A3 ? 1 : 0) << 3) |
                        ((_viewModel.Pins.A4 ? 1 : 0) << 4) |
                        ((_viewModel.Pins.A5 ? 1 : 0) << 5) |
                        ((_viewModel.Pins.A6 ? 1 : 0) << 6) |
                        ((_viewModel.Pins.A7 ? 1 : 0) << 7) |
                        ((_viewModel.Pins.A8 ? 1 : 0) << 8) |
                        ((_viewModel.Pins.A9 ? 1 : 0) << 9) |
                        ((_viewModel.Pins.A10 ? 1 : 0) << 10) |
                        ((_viewModel.Pins.A11 ? 1 : 0) << 11) |
                        ((_viewModel.Pins.A12 ? 1 : 0) << 12) |
                        ((_viewModel.Pins.A13 ? 1 : 0) << 13) |
                        ((_viewModel.Pins.A14 ? 1 : 0) << 14) |
                        ((_viewModel.Pins.A15 ? 1 : 0) << 15)
                    ),
                DataBus =
                    (byte)
                    (
                        ((_viewModel.Pins.D0 ? 1 : 0) << 0) |
                        ((_viewModel.Pins.D1 ? 1 : 0) << 1) |
                        ((_viewModel.Pins.D2 ? 1 : 0) << 2) |
                        ((_viewModel.Pins.D3 ? 1 : 0) << 3) |
                        ((_viewModel.Pins.D4 ? 1 : 0) << 4) |
                        ((_viewModel.Pins.D5 ? 1 : 0) << 5) |
                        ((_viewModel.Pins.D6 ? 1 : 0) << 6) |
                        ((_viewModel.Pins.D7 ? 1 : 0) << 7)
                    ),
                Other =
                    (byte)
                    (
                        ((_viewModel.Pins.Irq ? 1 : 0) << 0) |
                        ((_viewModel.Pins.Nmi ? 1 : 0) << 1) |
                        ((_viewModel.Pins.Rdy ? 1 : 0) << 2) |
                        ((_viewModel.Pins.Res ? 1 : 0) << 3) |
                        ((_viewModel.Pins.Rw ? 1 : 0) << 4) |
                        ((_viewModel.Pins.Sync ? 1 : 0) << 5) |
                        ((_viewModel.Pins.Vcc ? 1 : 0) << 6)
                    )
            };
            pinsPacket.RecalculateChecksum();

            return pinsPacket;
        }
    }
}
