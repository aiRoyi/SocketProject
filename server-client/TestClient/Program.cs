using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        private static byte[] buffer = new byte[1024];
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            Console.WriteLine("Connect to server");
            int receiveLength = clientSocket.Receive(buffer);
            Console.WriteLine("receive server message:" + Encoding.ASCII.GetString(buffer, 0, receiveLength));
            Console.WriteLine("input your message:");
            while(true)
            {
                string sendMessage = Console.ReadLine();
                if (sendMessage != null)
                {
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Console.WriteLine("send to server:" + sendMessage);
                }
            }
        }
    }
}
