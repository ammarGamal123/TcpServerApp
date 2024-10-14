using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServerApp
{
    public class Program
    {
        private const int Port = 5000; // Specify the port you want to listen on.
        private const string IpAddress = "127.0.0.1"; // Specify the IP address to listen on.

        static async Task Main(string[] args)
        {
            // Create a TCP listener using the specified IP address and port
            TcpListener listener = new TcpListener(IPAddress.Parse(IpAddress), Port);

            // Start listening for incoming connections
            listener.Start();
            Console.WriteLine($"Server is listening on {IpAddress}:{Port}...");

            while (true)
            {
                try
                {
                    // Accept a client connection
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected!");

                    // Handle the client in a separate task
                    _ = HandleClientAsync(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                try
                {
                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        // Convert the received bytes to a string
                        string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received: {receivedMessage}");

                        // Echo the message back to the client
                        byte[] response = Encoding.ASCII.GetBytes($"Server Echo: {receivedMessage}");
                        await stream.WriteAsync(response, 0, response.Length);
                        Console.WriteLine($"Sent: {Encoding.ASCII.GetString(response)}");
                    }
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
    }
}
