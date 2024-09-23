using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatP2P
{
    public class Peer
    {
        private readonly TcpListener _listener;
        private TcpClient? _client;
        private const int Port = 8080;

        public Peer() => _listener = new TcpListener(IPAddress.Any, Port);

        public async Task ConnectToPeer(string ipAddress, string port)
        {
            try
            {
                _client = new TcpClient(ipAddress, Convert.ToInt32(port));
                Console.WriteLine("Connected to peer :D");

                // Ejecutar la recepción y el envío de mensajes en paralelo
                var receiveTask = Task.Run(() => ReceiveMessage());
                var sendTask = Task.Run(() => SendMessage());

                await Task.WhenAll(receiveTask, sendTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection closed :c" + ex.Message);
            }
        }

        public async Task StartListening()
        {
            try
            {
                _listener.Start();
                Console.WriteLine("Listening for incoming connections...");
                _client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Connected to peer :D");

                // Ejecutar la recepción y el envío de mensajes en paralelo
                var receiveTask = Task.Run(() => ReceiveMessage());
                var sendTask = Task.Run(() => SendMessage());

                await Task.WhenAll(receiveTask, sendTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection closed :c" + ex.Message);
            }
        }

        private async Task ReceiveMessage()
        {
            try
            {
                var stream = _client!.GetStream();
                var reader = new StreamReader(stream, Encoding.UTF8);

                string? message;
                while ((message = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine($"Peer message: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
            }
            finally
            {
                Close();
            }
        }

        private async Task SendMessage()
        {
            try
            {
                var stream = _client!.GetStream();
                var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                while (true)
                {
                    var message = Console.ReadLine();
                    if (message == null) break;

                    await writer.WriteLineAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
            finally
            {
                Close();
            }
        }

        private void Close()
        {
            _client?.Close();
            _listener.Stop();
        }
    }
}