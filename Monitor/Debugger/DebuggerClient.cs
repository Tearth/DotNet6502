using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Monitor.Debugger.Handlers;
using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger
{
    public class DebuggerClient : IDisposable
    {
        public bool Connected => _tcpClient?.Connected ?? false;

        private TcpClient _tcpClient;
        private NetworkStream _tcpClientStream;
        private Task _clientTask;
        private MainWindowViewModel _viewModel;
        private PacketValidator _packetValidator;
        private PacketsFactory _packetsFactory;
        private Dictionary<PacketType, PacketHandlerBase> _packetHandler;

        public DebuggerClient(MainWindowViewModel viewModel)
        {
            _packetValidator = new PacketValidator();
            _viewModel = viewModel;

            _packetValidator = new PacketValidator();
            _packetsFactory = new PacketsFactory();

            _packetHandler = new Dictionary<PacketType, PacketHandlerBase>
            {
                { PacketType.Registers, new RegistersHandler(viewModel) },
                { PacketType.Pins, new PinsHandler(viewModel) }
            };
        }

        public async Task<(bool Success, string ErrorMessage)> Connect(string address)
        {
            try
            {
                var splitAddress = address.Split(':');
                var hostname = splitAddress[0];
                var port = int.Parse(splitAddress[1]);

                _tcpClient?.Dispose();
                _tcpClient = new TcpClient();

                await _tcpClient.ConnectAsync(hostname, port);
                _tcpClientStream = _tcpClient.GetStream();

                _clientTask = new Task(ClientLoop);
                _clientTask.Start();
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }

            return (true, null);
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }

        public void RequestForRegisters()
        {
            var requestPacket = new RegistersRequestPacket();
            requestPacket.RecalculateChecksum();

            _tcpClientStream.Write(requestPacket.Data, 0, requestPacket.Length);
        }

        public void UpdateRegisters()
        {
            var registersPacket = new RegistersPacket
            {
                ProgramCounter = _viewModel.Registers.Pc,
                StackPointer = _viewModel.Registers.Sp,
                Accumulator = _viewModel.Registers.Acc,
                XIndex = _viewModel.Registers.X,
                YIndex = _viewModel.Registers.Y,
                Flags = _viewModel.Registers.Flags
            };
            registersPacket.RecalculateChecksum();

            _tcpClientStream.Write(registersPacket.Data, 0, registersPacket.Length);
        }

        public void RequestForPins()
        {
            var requestPacket = new PinsRequestPacket();
            requestPacket.RecalculateChecksum();

            _tcpClientStream.Write(requestPacket.Data, 0, requestPacket.Length);
        }

        public void UpdatePins()
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
                        ((_viewModel.Pins.Nmi ? 1 : 0) << 6)
                    )
            };
            pinsPacket.RecalculateChecksum();

            _tcpClientStream.Write(pinsPacket.Data, 0, pinsPacket.Length);
        }

        private void ClientLoop()
        {
            var buffer = new byte[1024];
            var offset = 0;

            while (_tcpClient.Connected)
            {
                offset += _tcpClientStream.Read(buffer, offset, buffer.Length - offset);

                while (offset > 0)
                {
                    var validationResult = _packetValidator.Validate(buffer);
                    if (validationResult.Valid)
                    {
                        offset -= validationResult.Size;

                        var packet = _packetsFactory.Create(buffer.Take(validationResult.Size).ToArray());
                        if (packet.IsChecksumValid())
                        {
                            var response = _packetHandler[packet.Type].Handle(packet);
                            if (response != null)
                            {
                                _tcpClientStream.Write(response, 0, response.Length);
                            }
                        }

                        Array.Copy(buffer, validationResult.Size, buffer, 0, buffer.Length - validationResult.Size);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
