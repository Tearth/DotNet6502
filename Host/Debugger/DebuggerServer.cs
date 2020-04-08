﻿using System;
using System.Net;
using System.Net.Sockets;
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

                while (client.Connected)
                {
                    var buffer = new byte[1024];
                    clientStream.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}