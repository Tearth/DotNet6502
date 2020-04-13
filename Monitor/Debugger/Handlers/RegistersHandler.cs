﻿using System;
using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Handlers
{
    public class RegistersHandler : PacketHandlerBase
    {
        public RegistersHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var registersPacket = (RegistersPacket) packet;

            ViewModel.Registers.Pc = registersPacket.ProgramCounter;
            ViewModel.Registers.Sp = registersPacket.StackPointer;
            ViewModel.Registers.Acc = registersPacket.Accumulator;
            ViewModel.Registers.X = registersPacket.XIndex;
            ViewModel.Registers.Y = registersPacket.YIndex;
            ViewModel.Registers.Flags = registersPacket.Flags;

            return null;
        }
    }
}