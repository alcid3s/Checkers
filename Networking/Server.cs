using Checkers.board;
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
        public static string Setup = "1p1p1p1p1p/p1p1p1p1p1/1p1p1p1p1p/p1p1p1p1p1/10/10/1P1P1P1P1P/P1P1P1P1P1/1P1P1P1P1P/P1P1P1P1P1;";

        private Socket _serverSocket;
        private Client[] _clientList = new Client[2];

        private short _port;

        private Board _board;
        private string _currentState = string.Empty;
        private struct Client
        {
            public Piece.Side Side { get; private set; }
            public Socket Socket { get; }
            public byte PlayerId { get; }
            public Client(Socket socket, byte player, Piece.Side side)
            {
                Socket = socket;
                PlayerId = player;
                Side = side;
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
            _board = new Board();
            _board.Init(Setup);
            _currentState = Setup;
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

                    // Used to decide who plays as who.
                    bool whiteIsTaken = false;
                    if (_clientList.Length == 1)
                        whiteIsTaken = true;

                    // saving client to list if the socket of the client is not null.
                    if(socket != null)
                    {
                        // decides which side the player plays as.
                        Piece.Side side;
                        if (whiteIsTaken)
                            side = Piece.Side.Black;
                        else
                            side = Piece.Side.White;

                        Client client = new(socket, amountOfPlayersActive, side);
                        _clientList[amountOfPlayersActive] = client;
                        amountOfPlayersActive++;

                        // Every client gets its own thread.
                        new Thread(() =>
                        {
                            HandleClient(client);
                        }).Start();
                    }

                }
            }).Start();
        }

        private void HandleClient(Client client)
        {
            Console.WriteLine($"Client connection from: {client.Socket.RemoteEndPoint}");

            // Sends the basic setup to the client.
            UpdateBoard(client);
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

        private void UpdateBoard(Client client)
        {
            client.Socket.Send(Encoding.UTF8.GetBytes(_currentState + client.Side.ToString()));
        }
    }
}
