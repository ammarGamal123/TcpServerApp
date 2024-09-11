using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServerApp.Handlers;
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
            // Colorized output
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Server started at:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"IP Address: {ipAddress}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Port: {port}");
            Console.ResetColor();  // Inform the console that the server has started and is listening on the given IP and port.

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

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                byte[] receivedBytes = new byte[bytesRead];
                Buffer.BlockCopy(buffer, 0, receivedBytes, 0, bytesRead);

                // Define the delimiter used in the client
                byte[] delimiter = { 0x1D, 0x42, 0x1E }; // Example delimiter
                int delimiterLength = delimiter.Length;

                // Find the delimiter in the received data
                int delimiterIndex = IndexOf(receivedBytes, delimiter);

                if (delimiterIndex >= 0)
                {
                    // Extract command and message based on delimiter
                    byte[] commandBytes = new byte[delimiterIndex];
                    byte[] messageBytes = new byte[receivedBytes.Length - delimiterIndex - delimiterLength];

                    Buffer.BlockCopy(receivedBytes, 0, commandBytes, 0, delimiterIndex);
                    Buffer.BlockCopy(receivedBytes, delimiterIndex + delimiterLength, messageBytes, 0, messageBytes.Length);

                    // Convert the command bytes to a hexadecimal string
                    string commandHex = BitConverter.ToString(commandBytes).Replace("-", string.Empty);

                    // Map the command hex to its key
                    string commandKey = GetCommandKeyFromHex(commandHex);
                    // Color output
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Valid Command Received in Hex: {commandHex}");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Valid Command Received in String: {commandKey}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Message Received: {Encoding.ASCII.GetString(messageBytes)}");
                    Console.ResetColor();

                    // Process the command and message accordingly

                    // Send the response back to the client
                    byte[] responseBytes = Encoding.UTF8.GetBytes(_commandProcessor.ProcessCommand(commandBytes).ResponseMessage);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Delimiter not found in received data.");
                    Console.ResetColor();
                }
            }

            client.Close();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Client disconnected.");
            Console.ResetColor();
        }

        // Method to find the index of a byte pattern within a byte array
        private int IndexOf(byte[] source, byte[] pattern)
        {
            for (int i = 0; i <= source.Length - pattern.Length; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    return i;
                }
            }
            return -1;
        }

        // Method to get command key from hexadecimal value
        private string GetCommandKeyFromHex(string hexValue)
        {
            var commandDictionary = PrinterCommand.Commands;
            var commandEntry = commandDictionary.FirstOrDefault(x => x.Value.Equals(hexValue, StringComparison.OrdinalIgnoreCase));
            return commandEntry.Key ?? "Unknown Command";
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
