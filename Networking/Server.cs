using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Networking
{
    public class Server
    {
        private Socket _serverSocket;
        private Client[] _clientList = new Client[2];

        private short _port;
        private struct Client
        {
            public Socket? Socket { get; }
            public byte PlayerId { get; }
            public Client(Socket? socket, byte player)
            {
                Socket = socket;
                PlayerId = player;
            }
        }

        public Server(short port)
        {
            _port = port;

            _serverSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), port);

            _serverSocket.Bind(endPoint);
            _serverSocket.Listen(100);

            Console.WriteLine($"Server setup and running on {endPoint.Address}:{endPoint.Port}");
        }

        public void Run()
        {
            new Thread(() =>
            {
                byte amountOfPlayersActive = 0;
                while (true)
                {
                    // New incoming connection.
                    Socket? socket = null;
                    if (_serverSocket != null)
                        socket = _serverSocket.Accept();

                    // saving client to list.
                    Client client = new(socket, amountOfPlayersActive);
                    _clientList[amountOfPlayersActive] = client;
                    amountOfPlayersActive++;

                    // Every client gets its own thread.
                    new Thread(() =>
                    {
                        HandleClient(client);
                    }).Start();
                }
            }).Start();
        }

        private void HandleClient(Client client)
        {
            Console.WriteLine($"Client connection from: {client.Socket.RemoteEndPoint}");
            byte[] message = new byte[1024];

            while (client.Socket.Connected)
            {
                try
                {
                    int receive = client.Socket.Receive(message);
                    string information = Encoding.UTF8.GetString(message, 0, receive);

                    // TODO: Check if move player wants to do is legal.
                }catch(Exception e)
                {
                    Console.WriteLine($"Server Got error with Client {client.PlayerId}:\n{e}");
                }
            }

            client.Socket.Close();
        }
    }
}
