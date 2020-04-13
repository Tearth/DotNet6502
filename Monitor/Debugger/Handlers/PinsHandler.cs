using System;
using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Handlers
{
    public class PinsHandler : PacketHandlerBase
    {
        public PinsHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var pinsPacket = (PinsPacket)packet;

            ViewModel.Pins.A0  = Convert.ToBoolean((pinsPacket.AddressBus >> 0) & 1);
            ViewModel.Pins.A1  = Convert.ToBoolean((pinsPacket.AddressBus >> 1) & 1);
            ViewModel.Pins.A2  = Convert.ToBoolean((pinsPacket.AddressBus >> 2) & 1);
            ViewModel.Pins.A3  = Convert.ToBoolean((pinsPacket.AddressBus >> 3) & 1);
            ViewModel.Pins.A4  = Convert.ToBoolean((pinsPacket.AddressBus >> 4) & 1);
            ViewModel.Pins.A5  = Convert.ToBoolean((pinsPacket.AddressBus >> 5) & 1);
            ViewModel.Pins.A6  = Convert.ToBoolean((pinsPacket.AddressBus >> 6) & 1);
            ViewModel.Pins.A7  = Convert.ToBoolean((pinsPacket.AddressBus >> 7) & 1);
            ViewModel.Pins.A8  = Convert.ToBoolean((pinsPacket.AddressBus >> 8) & 1);
            ViewModel.Pins.A9  = Convert.ToBoolean((pinsPacket.AddressBus >> 9) & 1);
            ViewModel.Pins.A10 = Convert.ToBoolean((pinsPacket.AddressBus >> 10) & 1);
            ViewModel.Pins.A11 = Convert.ToBoolean((pinsPacket.AddressBus >> 11) & 1);
            ViewModel.Pins.A12 = Convert.ToBoolean((pinsPacket.AddressBus >> 12) & 1);
            ViewModel.Pins.A13 = Convert.ToBoolean((pinsPacket.AddressBus >> 13) & 1);
            ViewModel.Pins.A14 = Convert.ToBoolean((pinsPacket.AddressBus >> 14) & 1);
            ViewModel.Pins.A15 = Convert.ToBoolean((pinsPacket.AddressBus >> 15) & 1);

            ViewModel.Pins.D0 = Convert.ToBoolean((pinsPacket.DataBus >> 0) & 1);
            ViewModel.Pins.D1 = Convert.ToBoolean((pinsPacket.DataBus >> 1) & 1);
            ViewModel.Pins.D2 = Convert.ToBoolean((pinsPacket.DataBus >> 2) & 1);
            ViewModel.Pins.D3 = Convert.ToBoolean((pinsPacket.DataBus >> 3) & 1);
            ViewModel.Pins.D4 = Convert.ToBoolean((pinsPacket.DataBus >> 4) & 1);
            ViewModel.Pins.D5 = Convert.ToBoolean((pinsPacket.DataBus >> 5) & 1);
            ViewModel.Pins.D6 = Convert.ToBoolean((pinsPacket.DataBus >> 6) & 1);
            ViewModel.Pins.D7 = Convert.ToBoolean((pinsPacket.DataBus >> 7) & 1);

            ViewModel.Pins.Irq   = Convert.ToBoolean((pinsPacket.Other >> 0) & 1);
            ViewModel.Pins.Nmi   = Convert.ToBoolean((pinsPacket.Other >> 1) & 1);
            ViewModel.Pins.Rdy   = Convert.ToBoolean((pinsPacket.Other >> 2) & 1);
            ViewModel.Pins.Res   = Convert.ToBoolean((pinsPacket.Other >> 3) & 1);
            ViewModel.Pins.Rw    = Convert.ToBoolean((pinsPacket.Other >> 4) & 1);
            ViewModel.Pins.Sync  = Convert.ToBoolean((pinsPacket.Other >> 5) & 1);
            ViewModel.Pins.Vcc   = Convert.ToBoolean((pinsPacket.Other >> 6) & 1);

            return null;
        }
    }
}