using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CPU;

namespace Host.Debugger
{
    public class DebuggerServer : IDisposable
    {
        private Mos6502Core _core;
        private ushort _port;

        private Task _serverTask;
        private AutoResetEvent _initializationEnded;
        private TcpListener _tcpListener;

        public DebuggerServer(Mos6502Core core, ushort port)
        {
            _core = core;
            _port = port;
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

                var buffer = new byte[256];
                while (client.Connected)
                {
                    var x = clientStream.Read(buffer, 0, buffer.Length);
                    Console.WriteLine("Read data " + x.ToString());
                }

                Console.Write("Debugger client disconnected");
            }
        }
    }
}
