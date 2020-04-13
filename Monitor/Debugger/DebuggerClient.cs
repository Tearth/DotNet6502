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
                { PacketType.Registers, new RegistersHandler(viewModel) }
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
            var registersPacket = new RegistersPacket();
            registersPacket.ProgramCounter = _viewModel.Registers.Pc;
            registersPacket.StackPointer = _viewModel.Registers.Sp;
            registersPacket.Accumulator = _viewModel.Registers.Acc;
            registersPacket.XIndex = _viewModel.Registers.X;
            registersPacket.YIndex = _viewModel.Registers.Y;
            registersPacket.Flags = _viewModel.Registers.Flags;
            registersPacket.RecalculateChecksum();

            _tcpClientStream.Write(registersPacket.Data, 0, registersPacket.Length);
        }

        private void ClientLoop()
        {
            var buffer = new byte[1024];
            var offset = 0;

            while (_tcpClient.Connected)
            {
                offset += _tcpClientStream.Read(buffer, offset, buffer.Length);

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
            }
        }
    }
}
