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
    internal class Client
    {
        private short _port;
        private IPAddress _address;

        private Socket _socket;

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
            while (true)
            {
                byte[] message = new byte[1024];
                int receive = _socket.Receive(message);
                Console.WriteLine("Data received");
            }
        }

        public void Send(Vector2 posOfPiece, Vector2 newPosOfPiece)
        {
            string message = "" + posOfPiece.X + '-' + posOfPiece.Y + ':' + newPosOfPiece.X + '-' + newPosOfPiece;
            _socket.Send(Encoding.UTF8.GetBytes(message));
        }
    }
}
