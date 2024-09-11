using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServerApp.Interface;
using TcpServerApp.Interface;

namespace TcpServerApp.Services
{
    /// <summary>
    /// Responsible for managing the TCP server, accepting client connections, and processing incoming requests.
    /// </summary>
    public class TcpServer : ITcpServer
    {
        private TcpListener _tcpListener;  // A TcpListener object used to listen for incoming client connection requests.
        private readonly ICommandProcessor _commandProcessor;  // An instance of ICommandProcessor, used to handle the processing of commands sent by clients.

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServer"/> class.
        /// </summary>
        public TcpServer()
        {
            // Create a new CommandProcessor instance to process incoming commands from clients.
            _commandProcessor = new CommandProcessor();
        }

        /// <summary>
        /// Starts the TCP server to listen for incoming connections.
        /// </summary>
        /// <param name="ipAddress">The IP address to bind the server to.</param>
        /// <param name="port">The port to listen for incoming connections.</param>
        public void Start(string ipAddress, int port)
        {
            // Initialize the TcpListener with the specified IP address and port, which will listen for incoming client connections.
            _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);

            // Start the TcpListener, allowing it to begin listening for client requests.
            _tcpListener.Start();
            Console.WriteLine($"Server started at IP Address: {ipAddress}\nPort: {port}");  // Inform the console that the server has started and is listening on the given IP and port.

            // Begin listening for client connections asynchronously in a separate method.
            ListenForClientsAsync();
        }

        /// <summary>
        /// Asynchronously listens for client connections.
        /// </summary>
        private async void ListenForClientsAsync()
        {
            // Enter an infinite loop to continuously accept incoming client connections.
            while (true)
            {
                // Wait asynchronously for a client to connect to the server.
                TcpClient client = await _tcpListener.AcceptTcpClientAsync();

                // Notify that a new client has connected.
                Console.WriteLine("Client connected.");

                // Handle the client connection in a separate asynchronous method.
                HandleClientAsync(client);
            }
        }

        /// <summary>
        /// Handles client connection asynchronously by receiving and processing incoming data.
        /// </summary>
        /// <param name="client">The connected TCP client.</param>
        private async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Continuously read data from the client while it's sending.
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                // Create a new byte array that contains the exact amount of data read from the client.
                byte[] receivedBytes = new byte[bytesRead];
                Buffer.BlockCopy(buffer, 0, receivedBytes, 0, bytesRead);

                // Convert the received bytes to a hexadecimal string for identifying the command.
                string commandHex = BitConverter.ToString(receivedBytes).Replace("-", string.Empty);

                // Process the command instead of a general message.
                Console.WriteLine($"Valid Command Received in Hexa: {commandHex}");
                Console.WriteLine($"Valida Command Recieved in string: {HexToString(commandHex)}");

                // Process the command accordingly

                // Send the response back to the client
                byte[] responseBytes = Encoding.UTF8.GetBytes(_commandProcessor.ProcessCommand(receivedBytes).ResponseMessage);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }

            // Close the client connection when done.
            client.Close();
            Console.WriteLine("Client disconnected.");
        }

        private string HexToString(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Invalid hex string length.");
            }

            StringBuilder sb = new StringBuilder(hex.Length / 2);
            for (int i = 0; i < hex.Length; i += 2)
            {
                sb.Append((char)Convert.ToByte(hex.Substring(i, 2), 16));
            }

            return sb.ToString();
        }

    }
}
