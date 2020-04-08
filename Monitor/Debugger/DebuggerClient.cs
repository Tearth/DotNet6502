using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Monitor.Debugger
{
    public class DebuggerClient
    {
        private TcpClient _tcpClient;

        public DebuggerClient()
        {
            _tcpClient = new TcpClient();
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
    }
}
