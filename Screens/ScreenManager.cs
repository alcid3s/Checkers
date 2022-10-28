using Checkers.board;
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
    internal class ScreenManager : Screen
    {
        public enum ScreenState
        {
            MainScreenState,
            HostOrJoinState,
            HostState,
            JoinState,
            PlayState,
            PlayStateWithServer
        }

        public static ScreenState State = ScreenState.MainScreenState;
        public static Board Board = new();

        private Color _backGround;

        private MainScreen _mainScreen = new(Color.LIME);
        private HostOrJoinScreen _hostOrJoinScreen = new();


        private Server? _server = null;
        private Client? _client = null;

        private bool _firstTimeRunBoard = true;

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
                    Console.WriteLine("lol");
                    break;
                case ScreenState.HostOrJoinState:
                    _hostOrJoinScreen.Update();
                    break;
                case ScreenState.HostState:
                    break;
                case ScreenState.JoinState:
                    break;
                case ScreenState.PlayState:
                    Board.Update();
                    break;
                case ScreenState.PlayStateWithServer:
                    if (_firstTimeRunBoard)
                    {
                        _server = new Server(1337);
                        _server.Run();
                        _firstTimeRunBoard = false;
                        _client = new Client("127.0.0.1", 1337);
                        _client.Connect();
                        _firstTimeRunBoard = false;
                    }
                    else if (Board.HasFen != string.Empty && !Board.HasInitialised)
                        Board.Init(Board.HasFen);
                    else if (Board.HasInitialised)
                        Board.Update();
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
                    break;
                case ScreenState.PlayState:
                    ClearBackground(_backGround);
                    Board.Draw();
                    break;
                case ScreenState.PlayStateWithServer:
                    ClearBackground(_backGround);
                    if(Board.HasInitialised)
                        Board.Draw();
                    break;
            }
        }
    }
}
