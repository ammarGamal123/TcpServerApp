using System.Net;
using System.Net.Sockets;
using TcpServerApp.Interface;
using TcpServerApp.Services;

namespace TcpServerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            int port = 9100;

            string ipAddress = "127.0.0.1";

            ITcpServer tcpServer = new TcpServer();

            tcpServer.Start(ipAddress, port);




            Console.ReadKey();
        }
    }
}
