using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCmd
{
    class Program
    {
        private static ManualResetEvent resetEvent = new ManualResetEvent(false);
        private static UdpClient _udpClient;

        private static void Send(string hostname, int port, string msg)
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(IPAddress.Parse(hostname), port);

            Encoding encoding = Encoding.ASCII;
            byte[] msgBytes = new byte[msg.Length];
            encoding.GetBytes(msg, 0, msg.Length, msgBytes, 0);
            int bytes = udpClient.Send(msgBytes, msgBytes.Length);
            udpClient.Close();
        }

        static void Main(string[] args)
        {
            int receivePort = int.Parse(args[2]);
            Receive(receivePort);

            string hostname = args[0];
            int sendPort = int.Parse(args[1]);
            int sendTimes = args.Length >= 4 ? int.Parse(args[3]) : 1;
            int sendMillis = args.Length >= 5 ? int.Parse(args[4]) : 0;

            string msg = string.Empty;
            while (msg != "q!")
            {
                msg = Console.ReadLine();
                for (int i = 0; i < sendTimes && msg != "q!"; i++)
                {
                    if (sendMillis != 0)
                    {
                        Thread.Sleep(sendMillis);
                    }

                    string inMsg = msg.Replace("]", $"- {i}]");
                    Send(hostname, sendPort, inMsg);
                    Console.WriteLine($"out --> {inMsg}");
                }
            }
        }

        private static void Receive(int receivePort)
        {
            _udpClient = new UdpClient(receivePort);
            _udpClient.BeginReceive(ReceiveAsynch, _udpClient);
        }

        private static void ReceiveAsynch(IAsyncResult ar)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = _udpClient.EndReceive(ar, ref endPoint);
            string msg = Encoding.ASCII.GetString(bytes);

            Console.WriteLine($"in <-- {msg}");
            _udpClient.BeginReceive(ReceiveAsynch, _udpClient);
        }
    }
}
