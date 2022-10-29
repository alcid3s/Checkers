using Checkers.board;
using Checkers.pieces;
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
        // Used for setup.
        public static string Setup = "1p1p1p1p1p/p1p1p1p1p1/1p1p1p1p1p/p1p1p1p1p1/10/10/1P1P1P1P1P/P1P1P1P1P1/1P1P1P1P1P/P1P1P1P1P1;";
        private bool _whiteIsTaken = false;

        // Variables for networking.
        private readonly Socket _serverSocket;
        private Client[] _clientList = new Client[2]; 
        private readonly short _port = 1337;

        private readonly Board _board;
        private readonly string _currentState = string.Empty;

        public static byte AmountOfPlayersActive { get; private set; } = 0;

        public struct Client
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
            _board = new Board(false);
            _board.Init(Setup);
            _currentState = Setup;
        }

        public void Run()
        {
            new Thread(() =>
            {
                while (true)
                {
                    // New incoming connection.
                    Socket? socket = null;
                    if (_serverSocket != null)
                        socket = _serverSocket.Accept();

                    // saving client to list if the socket of the client is not null.
                    if (socket != null)
                    {
                        // decides which side the player plays as.
                        Piece.Side side;
                        if (_whiteIsTaken)
                            side = Piece.Side.Black;
                        else
                        {
                            side = Piece.Side.White;
                            _whiteIsTaken = true;
                        }
                            

                        Client client = new(socket, AmountOfPlayersActive, side);
                        _clientList[AmountOfPlayersActive] = client;
                        AmountOfPlayersActive++;

                        Console.WriteLine($"SERVER: Amount of players is now: {AmountOfPlayersActive}");

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
            Console.WriteLine($"SERVER: Client connection from: {client.Socket.RemoteEndPoint}");

            // Sends the basic setup to the client.
            SendBasicData(client);

            Console.WriteLine("------------------EOL OF CLIENT 2-------------------");
            byte[] message = new byte[1024];

            while (client.Socket.Connected)
            {
                try
                {
                    int receive = client.Socket.Receive(message);
                    string information = Encoding.UTF8.GetString(message, 0, receive);

                    // If the player who has the turn also makes a move
                    if (client.Side.Equals(_board.Manager.WhoseTurn))
                    {
                        Console.WriteLine($"SERVER: DATA FROM CLIENT: {information}");

                        (int, string, int) data = _board.ParseMessage(information);

                        //Check if move player wants to do is legal.
                        if (_board.IsLegalMove(data.Item1, data.Item2, data.Item3))
                        {
                            _board.PositionSelected = new(_board.Tiles[data.Item1], _board.Tiles[data.Item1].Piece);

                            // This thread makes sure the piece also gets moved on the board the server holds.
                            new Thread(() =>
                            {
                                _board.ChangePosition(_board.Tiles[data.Item3]);
                            }).Start();

                            // The move was legal so this updates the board for server and client that sended the information.
                            client.Socket.Send(Encoding.UTF8.GetBytes("T"));


                            foreach (Client c in _clientList)
                            {
                                // Send the newly made moves to the other client
                                if (!c.Equals(client)
                                {
                                    Console.WriteLine($"SERVER: SENDING NEW POSITION TO {c.Socket.RemoteEndPoint}, NEW MESSAGE CONTAINS: {information}");
                                    client.Socket.Send(Encoding.UTF8.GetBytes(information));
                                }
                            }

                            information = string.Empty;

                            // Change turn to other party.
                            _board.Manager.ToggleTurns();
                        }
                        else
                        {
                            client.Socket.Send(Encoding.UTF8.GetBytes("F"));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Server got error with Client {client.PlayerId}:\n{e}");
                }
            }

            client.Socket.Close();
        }

        private void SendBasicData(Client client)
        {
            Console.WriteLine($"SERVER: Sending basic setup to {client.Socket.RemoteEndPoint}");
            if (client.Side.Equals(Piece.Side.White))
                client.Socket.Send(Encoding.UTF8.GetBytes(_currentState + 'W'));
            else if(client.Side.Equals(Piece.Side.Black))
                client.Socket.Send(Encoding.UTF8.GetBytes(_currentState + 'B'));
        }
    }
}
