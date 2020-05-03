using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Host.Debugger.Handlers.Commands;
using Host.Debugger.Handlers.Requests;
using Host.Debugger.Handlers.Responses;
using M6502;
using Protocol.Packets;

namespace Host.Debugger
{
    public class DebuggerServer : IDisposable
    {
        private readonly ushort _port;

        private Task _serverTask;
        private AutoResetEvent _initializationEnded;
        private TcpListener _tcpListener;
        private readonly PacketValidator _packetValidator;
        private readonly PacketsFactory _packetsFactory;
        private readonly Dictionary<PacketType, PacketHandlerBase> _packetHandler;

        public DebuggerServer(M6502Core core, ushort port)
        {
            _port = port;
            _packetValidator = new PacketValidator();
            _packetsFactory = new PacketsFactory();

            _packetHandler = new Dictionary<PacketType, PacketHandlerBase>
            {
                { PacketType.RegistersRequest, new RegistersRequestHandler(core) },
                { PacketType.Registers, new RegistersHandler(core) },
                { PacketType.PinsRequest, new PinsRequestHandler(core) },
                { PacketType.Pins, new PinsHandler(core) },
                { PacketType.CyclesRequest, new CyclesRequestHandler(core) },
                { PacketType.StopCommand, new StopCommandHandler(core) },
                { PacketType.ContinueCommand, new ContinueCommandHandler(core) },
                { PacketType.NextCycleCommand, new NextCycleCommandHandler(core) },
                { PacketType.NextInstructionCommand, new NextInstructionCommandHandler(core) },
                { PacketType.MemoryRequest, new MemoryRequestHandler(core) },
                { PacketType.RunToAddressCommand, new RunToAddressCommandHandler(core) },
                { PacketType.RunUntilLoopCommand, new RunUntilLoopCommandHandler(core) }
            };
        }

        public void Start()
        {
            _initializationEnded = new AutoResetEvent(false);
            _serverTask = new Task(ServerLoop);
            _serverTask.Start();

            _initializationEnded.WaitOne();
        }

        public void Dispose()
        {
            _tcpListener.Stop();
        }

        private void ServerLoop()
        {
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _initializationEnded.Set();

            while (true)
            {
                var client = _tcpListener.AcceptTcpClient();
                var clientStream = client.GetStream();

                Console.WriteLine($"New debugger client connected: {client.Client.RemoteEndPoint}");

                var buffer = new byte[2048];
                var offset = 0;

                while (client.Connected)
                {
                    offset += clientStream.Read(buffer, offset, buffer.Length - offset);

                    while (offset > 0)
                    {
                        var validationResult = _packetValidator.Validate(buffer);
                        if (validationResult.Valid)
                        {
                            offset -= validationResult.Size;

                            var packet = _packetsFactory.Create(buffer.Take(validationResult.Size).ToArray());
                            if (packet.IsChecksumValid())
                            {
                                var responsePacket = _packetHandler[packet.Type].Handle(packet);
                                if (responsePacket != null)
                                {
                                    clientStream.Write(responsePacket.Data, 0, responsePacket.Data.Length);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid checksum detected");
                            }

                            Array.Copy(buffer, validationResult.Size, buffer, 0, buffer.Length - validationResult.Size);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Console.Write("Debugger client disconnected");
            }
        }
    }
}
