using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Program
    {
        private static byte[] buffer = new byte[1024];
        private static int port = 8888;
        static Socket serverSocket;
        static void Main(string[] args)
        {
            serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            serverSocket.Listen(10);
            Thread thread = new Thread(ListenClientConnect);
            thread.Start();
            Console.ReadLine();
        }

        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }

        private static void ReceiveMessage(object socket)
        {
            Socket clientSocket = (Socket)socket;
            while (true)
            {
                try
                {
                    int receiveMsg = clientSocket.Receive(buffer);
                    Console.WriteLine("receive client message:"+Encoding.ASCII.GetString(buffer,0,receiveMsg));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
