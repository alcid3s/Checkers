using Checkers.Screens;
using Microsoft.VisualBasic;
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
        private readonly int _port;
        private readonly IPAddress _address;

        private static Socket? _socket;

        public Client(string ip, int port)
        {
            _address = IPAddress.Parse(ip);
            _port = port;
        }

        public void Connect()
        {
            Console.WriteLine("CLIENT: CONNECTING  WITH SERVER");
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
            Console.WriteLine("CLIENT: LISTENING");
            if (_socket != null)
            {
                while (true)
                {
                    byte[] message = new byte[1024];
                    _socket.Receive(message);
                    string response = Encoding.UTF8.GetString(message);
                    Console.WriteLine($"CLIENT: Data received: {response}");

                    if (firstMessage)
                    {
                        Console.WriteLine("CLIENT: Initialising FEN");
                        ScreenManager.Board.HasFen = response;
                        firstMessage = false;
                    }
                    else
                    {
                        // If a response is an update on the current position of the opposite player.
                        if (response.Contains(":"))
                        {
                            (int, string, int) data = ScreenManager.Board.ParseMessage(response);
                            ScreenManager.Board.PositionSelected = new(ScreenManager.Board.Tiles[data.Item1], ScreenManager.Board.Tiles[data.Item1].Piece);

                            Console.WriteLine(data.Item1 + "-" + data.Item3);
                            ScreenManager.Board.ChangePosition(ScreenManager.Board.Tiles[data.Item3]);
                        }

                        // If your own move has been validated
                        else if (response.Contains("T"))
                        {
                            ScreenManager.Board.GotReply = board.Board.Reply.TRUE;
                        }
                        else if (response.Contains("F"))
                        {
                            ScreenManager.Board.GotReply = board.Board.Reply.FALSE;
                        }
                    }
                }
            }
        }

        public static Task Send(string message)
        {
            if (_socket != null)
                _ = _socket.SendAsync(Encoding.UTF8.GetBytes(message), SocketFlags.Partial);
            return Task.CompletedTask;
        }
    }
}
