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

namespace Checkers.Screens
{
    internal class ScreenManager : Screen
    {
        public enum ScreenState {
            MainScreenState,
            HostOrJoinState,
            HostState,
            JoinState,
            PlayState
        }

        public static string Setup = "1p1p1p1p1p/p1p1p1p1p1/1p1p1p1p1p/p1p1p1p1p1/10/10/1P1P1P1P1P/P1P1P1P1P1/1P1P1P1P1P/P1P1P1P1P1";

        public static ScreenState State = ScreenState.MainScreenState;
        private Color _backGround;

        private MainScreen _mainScreen = new(Color.LIME);
        private HostOrJoinScreen _hostOrJoinScreen = new();
        private Board _board = new Board();


        public ScreenManager(Color backGround)
        {
            _backGround = backGround;
            _board.Init(Setup);
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
                    _board.Draw();
                    break;
            }
        }

        public override void Update()
        {
            switch (State)
            {
                case ScreenState.MainScreenState:
                    _mainScreen.Update();
                    break;
                case ScreenState.HostOrJoinState:
                    _hostOrJoinScreen.Update();
                    break;
                case ScreenState.HostState:
                    break;
                case ScreenState.JoinState:
                    break;
                case ScreenState.PlayState:
                    _board.Update();
                    break;
            }
        }
    }
}
