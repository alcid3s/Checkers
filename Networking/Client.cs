using Checkers.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Networking
{
    public class Client
    {
        private readonly short _port;
        private readonly IPAddress _address;

        private static Socket? _socket;

        public Client(string ip, short port)
        {
            _address = IPAddress.Parse(ip);
            _port = port;
        }

        public void Connect()
        {
            IPEndPoint endPoint = new(_address, _port);
            _socket = new Socket(_address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _socket.Connect(endPoint);
                new Thread(Listen).Start();
                Console.WriteLine($"Connecting to {_socket.RemoteEndPoint}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception connecting Client to server:\n{e}");
            }
        }

        private void Listen()
        {
            bool firstMessage = true;
            while (true)
            {
                if(_socket != null)
                {
                    byte[] message = new byte[1024];
                    _socket.Receive(message);
                    string response = Encoding.UTF8.GetString(message);
                    Console.WriteLine($"Data received: {response}");

                    if (firstMessage)
                    {
                        ScreenManager.Board.HasFen = response;
                        firstMessage = false;
                    }
                }
            }
        }

        public static void Send(string message)
        {
            if(_socket != null)
                _socket.Send(Encoding.UTF8.GetBytes(message));
        }
    }
}
