using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Monitor.Debugger.Generators.Responses
{
    public class PinsPacketGenerator
    {
        public PacketBase Generate(PinsViewModel pinsViewModel)
        {
            var pinsPacket = new PinsPacket
            {
                AddressBus =
                    (ushort)
                    (
                        ((pinsViewModel.A0 ? 1 : 0) << 0) |
                        ((pinsViewModel.A1 ? 1 : 0) << 1) |
                        ((pinsViewModel.A2 ? 1 : 0) << 2) |
                        ((pinsViewModel.A3 ? 1 : 0) << 3) |
                        ((pinsViewModel.A4 ? 1 : 0) << 4) |
                        ((pinsViewModel.A5 ? 1 : 0) << 5) |
                        ((pinsViewModel.A6 ? 1 : 0) << 6) |
                        ((pinsViewModel.A7 ? 1 : 0) << 7) |
                        ((pinsViewModel.A8 ? 1 : 0) << 8) |
                        ((pinsViewModel.A9 ? 1 : 0) << 9) |
                        ((pinsViewModel.A10 ? 1 : 0) << 10) |
                        ((pinsViewModel.A11 ? 1 : 0) << 11) |
                        ((pinsViewModel.A12 ? 1 : 0) << 12) |
                        ((pinsViewModel.A13 ? 1 : 0) << 13) |
                        ((pinsViewModel.A14 ? 1 : 0) << 14) |
                        ((pinsViewModel.A15 ? 1 : 0) << 15)
                    ),
                DataBus =
                    (byte)
                    (
                        ((pinsViewModel.D0 ? 1 : 0) << 0) |
                        ((pinsViewModel.D1 ? 1 : 0) << 1) |
                        ((pinsViewModel.D2 ? 1 : 0) << 2) |
                        ((pinsViewModel.D3 ? 1 : 0) << 3) |
                        ((pinsViewModel.D4 ? 1 : 0) << 4) |
                        ((pinsViewModel.D5 ? 1 : 0) << 5) |
                        ((pinsViewModel.D6 ? 1 : 0) << 6) |
                        ((pinsViewModel.D7 ? 1 : 0) << 7)
                    ),
                Other =
                    (byte)
                    (
                        ((pinsViewModel.Irq ? 1 : 0) << 0) |
                        ((pinsViewModel.Nmi ? 1 : 0) << 1) |
                        ((pinsViewModel.Rdy ? 1 : 0) << 2) |
                        ((pinsViewModel.Res ? 1 : 0) << 3) |
                        ((pinsViewModel.Rw ? 1 : 0) << 4) |
                        ((pinsViewModel.Sync ? 1 : 0) << 5) |
                        ((pinsViewModel.Vcc ? 1 : 0) << 6)
                    )
            };
            pinsPacket.RecalculateChecksum();

            return pinsPacket;
        }
    }
}
