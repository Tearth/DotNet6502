using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CPU;
using Host.Debugger.PacketHandlers;
using Protocol.Packets;

namespace Host.Debugger
{
    public class DebuggerServer : IDisposable
    {
        private Mos6502Core _core;
        private ushort _port;

        private Task _serverTask;
        private AutoResetEvent _initializationEnded;
        private TcpListener _tcpListener;
        private PacketValidator _packetValidator;
        private PacketsFactory _packetsFactory;
        private Dictionary<PacketType, PacketHandlerBase> _packetHandler;

        public DebuggerServer(Mos6502Core core, ushort port)
        {
            _core = core;
            _port = port;
            _packetValidator = new PacketValidator();
            _packetsFactory = new PacketsFactory();

            _packetHandler = new Dictionary<PacketType, PacketHandlerBase>
            {
                { PacketType.RegistersRequest, new RegistersRequestHandler(_core) }
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

                var buffer = new byte[1024];
                var offset = 0;

                while (client.Connected)
                {
                    offset += clientStream.Read(buffer, offset, buffer.Length);
                    
                    var validationResult = _packetValidator.Validate(buffer);
                    if (validationResult.Valid)
                    {
                        offset -= validationResult.Size;

                        var packet = _packetsFactory.Create(buffer);
                        var response = _packetHandler[packet.Type].Handle();
                        if (response != null)
                        {
                            clientStream.Write(response, 0, response.Length);
                        }

                        Array.Copy(buffer, validationResult.Size, buffer, 0, buffer.Length - validationResult.Size);
                    }
                }

                Console.Write("Debugger client disconnected");
            }
        }
    }
}
