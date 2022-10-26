using Checkers.graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Screens
{
    internal class ScreenManager : Screen
    {
        public enum State{
            MainScreenState,
            HostOrJoinState,
            HostState,
            JoinState,
            PlayState
        }

        private State _state = State.MainScreenState;
        List<Screen> _screenList = new List<Screen>();

        public ScreenManager()
        {
            _screenList.Add(new MainScreen());
            _screenList.Add(new HostOrJoinScreen());
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            switch (_state)
            {
                case State.MainScreenState:
                    _screenList.Find(x => typeof(MainScreen).Equals(x)).Update();
                    break;
                case State.HostOrJoinState:
                    _screenList.Find(x => typeof(HostOrJoinScreen).Equals(x)).Update();
                    break;
                case State.HostState:
                    break;
                case State.JoinState:
                    break;
                case State.PlayState:
                    break;
            }
        }
    }
}
