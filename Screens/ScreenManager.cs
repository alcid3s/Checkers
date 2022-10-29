﻿using Checkers.board;
using Checkers.graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Checkers.Screens.ScreenManager;
using Checkers.Networking;

namespace Checkers.Screens
{
    public class ScreenManager : Screen
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public static string Path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString() + "/SavedGames";
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        private string _suffix = ".txt";
        public enum ScreenState
        {
            MainScreenState,
            HostOrJoinState,
            HostState,
            JoinState,
            WaitState,
            PlayState,
            SetupServer
        }

        public static ScreenState State = ScreenState.MainScreenState;
        public static Board Board = new(true);

        private Color _backGround;

        private MainScreen _mainScreen = new(Color.LIME);
        private HostOrJoinScreen _hostOrJoinScreen = new();
        private JoinScreen _joinScreen = new();

        private WaitForPlayerScreen _waitScreen = new();


        private Server? _server = null;
        private Client? _client = null;

        private bool _firstTimeRunBoard = true;
        private bool _firstTimeRunHostOrJoin = true;

        public ScreenManager(Color backGround)
        {
            _backGround = backGround;
        }
        public override void Update()
        {
            switch (State)
            {
                case ScreenState.MainScreenState:
                    _mainScreen.Update();
                    break;

                case ScreenState.HostOrJoinState:

                    // New thread goes here because it comes after MainScreen, and mainscreen holds SaveData state.
                    if (_firstTimeRunHostOrJoin)
                    {
                        new Thread(SaveData).Start();
                        _firstTimeRunHostOrJoin = false;
                    }
                    

                    _hostOrJoinScreen.Update();
                    break;

                case ScreenState.HostState:
                    break;

                case ScreenState.JoinState:
                    _joinScreen.Update();
                    break;

                case ScreenState.WaitState:
                    if (_waitScreen.Ready)
                        _waitScreen.Update();
                    break;
                    
                case ScreenState.PlayState:
                    if (_firstTimeRunBoard)
                    {
                        _client = new Client(_joinScreen.Ip, _joinScreen.Port);
                        _client.Connect();
                        _firstTimeRunBoard = false;
                    }
                    else if (Board.HasFen != String.Empty && !Board.HasInitialised)
                        Board.Init(Board.HasFen);
                    else if(Board.HasInitialised)
                        Board.Update();
                    break;

                case ScreenState.SetupServer:
                    if (_firstTimeRunBoard)
                    {
                        _server = new Server(1337);
                        _server.Run();
                        _firstTimeRunBoard = false;

                        _client = new Client("127.0.0.1", 1337);

                        // Gets FEN string from server
                        _client.Connect();

                        _firstTimeRunBoard = false;
                    }
                    
                    else if (Board.HasFen != string.Empty && !Board.HasInitialised)
                    {
                        Board.Init(Board.HasFen);
                    }
                        
                    
                    else if (_server != null && Board.HasInitialised)
                        if (Server.AmountOfPlayersActive != 2)
                        {
                            State = ScreenState.WaitState;
                        }
                            
                        else
                            State = ScreenState.PlayState;

                    break;
            }
        }

        public override void Draw()
        {
            switch (State)
            {
                case ScreenState.MainScreenState:
                    ClearBackground(_backGround);
                    _mainScreen.Draw();
                    break;

                case ScreenState.HostOrJoinState:
                    ClearBackground(_backGround);
                    _hostOrJoinScreen.Draw();
                    break;

                case ScreenState.HostState:
                    ClearBackground(_backGround);
                    break;

                case ScreenState.JoinState:
                    ClearBackground(_backGround);
                    _joinScreen.Draw();
                    break;

                case ScreenState.WaitState:
                    ClearBackground(_backGround);
                    if (_waitScreen.Ready)
                        _waitScreen.Draw();
                    break;

                case ScreenState.PlayState:
                    ClearBackground(_backGround);
                    if(Board.HasInitialised)
                        Board.Draw();
                    break;

                case ScreenState.SetupServer:
                    ClearBackground(_backGround);
                    break;
            }
        }

        private void SaveData()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            string fileName = CreateFileName();
            Console.WriteLine(fileName);
            var fs = File.Create(Path + fileName + _suffix);
            var sr = new StreamWriter(fs);

            string currentInformation = string.Empty;
            // While the game isn't finished
            while (true)
            {
                if (!currentInformation.Equals(Board.NewMove))
                {
                    currentInformation = Board.NewMove;
                    sr.WriteLine(currentInformation);
                }
            }

            // game has finished
            sr.Close();
        }

        private string CreateFileName()
        {
            return $"/Checkers_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}_{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
        }
    }
}
