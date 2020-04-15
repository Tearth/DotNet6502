using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Monitor.Debugger.Generators;
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

        private readonly PinsRequestPacketGenerator _pinsRequestPacketGenerator;
        private readonly PinsPacketGenerator _pinsPacketGenerator;
        private readonly RegistersRequestPacketGenerator _registersRequestPacketGenerator;
        private readonly RegistersPacketGenerator _registersPacketGenerator;
        private readonly CyclesRequestPacketGenerator _cyclesRequestPacketGenerator;
        private readonly StopCommandPacketGenerator _stopCommandPacketGenerator;
        private readonly ContinueCommandPacketGenerator _continueCommandPacketGenerator;
        private readonly NextCommandPacketGenerator _nextCommandPacketGenerator;

        public DebuggerClient(MainWindowViewModel viewModel)
        {
            _packetValidator = new PacketValidator();
            _viewModel = viewModel;

            _packetValidator = new PacketValidator();
            _packetsFactory = new PacketsFactory();

            _packetHandler = new Dictionary<PacketType, PacketHandlerBase>
            {
                { PacketType.Registers, new RegistersHandler(viewModel) },
                { PacketType.Pins, new PinsHandler(viewModel) },
                { PacketType.Cycles, new CyclesHandler(viewModel) }
            };

            _pinsRequestPacketGenerator = new PinsRequestPacketGenerator(viewModel);
            _pinsPacketGenerator = new PinsPacketGenerator(viewModel);
            _registersRequestPacketGenerator = new RegistersRequestPacketGenerator(viewModel);
            _registersPacketGenerator = new RegistersPacketGenerator(viewModel);
            _cyclesRequestPacketGenerator = new CyclesRequestPacketGenerator(viewModel);
            _stopCommandPacketGenerator = new StopCommandPacketGenerator(viewModel);
            _continueCommandPacketGenerator = new ContinueCommandPacketGenerator(viewModel);
            _nextCommandPacketGenerator = new NextCommandPacketGenerator(viewModel);
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
            var packet = _registersRequestPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void UpdateRegisters()
        {
            var packet = _registersPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void RequestForPins()
        {
            var packet = _pinsRequestPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void UpdatePins()
        {
            var packet = _pinsPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void RequestForCycles()
        {
            var packet = _cyclesRequestPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void SendStopCommand()
        {
            var packet = _stopCommandPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void SendContinueCommand()
        {
            var packet = _continueCommandPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
        }

        public void SendNextCommand()
        {
            var packet = _nextCommandPacketGenerator.Generate();
            _tcpClientStream.Write(packet.Data, 0, packet.Length);
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
