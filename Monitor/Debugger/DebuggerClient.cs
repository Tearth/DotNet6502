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
        private TcpClient _tcpClient;
        private Task _clientTask;
        private MainWindowViewModel _viewModel;
        private PacketValidator _packetValidator;
        private PacketsFactory _packetsFactory;
        private Dictionary<PacketType, PacketHandlerBase> _packetHandler;

        public DebuggerClient(MainWindowViewModel viewModel)
        {
            _tcpClient = new TcpClient();
            _packetValidator = new PacketValidator();
            _viewModel = viewModel;

            _packetValidator = new PacketValidator();
            _packetsFactory = new PacketsFactory();

            _packetHandler = new Dictionary<PacketType, PacketHandlerBase>
            {
                { PacketType.Registers, new RegistersResponseHandler(viewModel) }
            };
        }

        public async Task<(bool Success, string ErrorMessage)> Connect(string address)
        {
            try
            {
                var splitAddress = address.Split(':');
                var hostname = splitAddress[0];
                var port = int.Parse(splitAddress[1]);

                await _tcpClient.ConnectAsync(hostname, port);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }

            return (true, null);
        }

        public void Run()
        {
            _clientTask = new Task(ClientLoop);
            _clientTask.Start();
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }

        public void RequestForRegisters()
        {
            var requestPacket = new RegistersRequestPacket();
            requestPacket.RecalculateChecksum();

            _tcpClient.GetStream().Write(requestPacket.Data, 0, requestPacket.Length);
        }

        private void ClientLoop()
        {
            var clientStream = _tcpClient.GetStream();
            var buffer = new byte[1024];
            var offset = 0;

            while (_tcpClient.Connected)
            {
                offset += clientStream.Read(buffer, offset, buffer.Length);

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
                            clientStream.Write(response, 0, response.Length);
                        }
                    }

                    Array.Copy(buffer, validationResult.Size, buffer, 0, buffer.Length - validationResult.Size);
                }
            }
        }
    }
}
