using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServerApp
{
    public class Program
    {
        private const int Port = 5000; // The port number to listen on.
        private const string IpAddress = "127.0.0.1"; // The IP address to listen on.

        /// <summary>
        /// Entry point of the application.
        /// Initializes and starts the TCP server.
        /// </summary>
        static async Task Main(string[] args)
        {
            TcpListener listener = CreateTcpListener();
            StartListener(listener);

            await ListenForClientsAsync(listener);
        }

        /// <summary>
        /// Creates a TCP listener for the specified IP address and port.
        /// </summary>
        /// <returns>A configured <see cref="TcpListener"/> instance.</returns>
        private static TcpListener CreateTcpListener()
        {
            return new TcpListener(IPAddress.Parse(IpAddress), Port);
        }

        /// <summary>
        /// Starts the TCP listener to begin accepting connections.
        /// </summary>
        /// <param name="listener">The <see cref="TcpListener"/> to start.</param>
        private static void StartListener(TcpListener listener)
        {
            listener.Start();
            Console.WriteLine($"Server is listening on {IpAddress}:{Port}...");
        }

        /// <summary>
        /// Continuously listens for incoming client connections.
        /// </summary>
        /// <param name="listener">The active <see cref="TcpListener"/> instance.</param>
        private static async Task ListenForClientsAsync(TcpListener listener)
        {
            while (true)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected!");

                    _ = HandleClientAsync(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handles communication with a connected client.
        /// </summary>
        /// <param name="client">The <see cref="TcpClient"/> instance representing the connected client.</param>
        private static async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                try
                {
                    await CommunicateWithClientAsync(stream, buffer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling client: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Client disconnected.");
                }
            }
        }

        /// <summary>
        /// Continuously communicates with the connected client, receiving and echoing messages.
        /// </summary>
        /// <param name="stream">The <see cref="NetworkStream"/> for reading and writing data.</param>
        /// <param name="buffer">The byte buffer for storing incoming data.</param>
        private static async Task CommunicateWithClientAsync(NetworkStream stream, byte[] buffer)
        {
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedMessage}");

                await SendResponseAsync(stream, $"Server Echo: {receivedMessage}");
            }
        }

        /// <summary>
        /// Sends a response message to the connected client.
        /// </summary>
        /// <param name="stream">The <see cref="NetworkStream"/> for writing data.</param>
        /// <param name="message">The message to send to the client.</param>
        private static async Task SendResponseAsync(NetworkStream stream, string message)
        {
            byte[] response = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(response, 0, response.Length);
            Console.WriteLine($"Sent: {message}");
        }
    }
}
