using Checkers.Custom;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Screens
{
    public class HostOrJoinScreen : Screen
    {
        Button _hostbtn;
        Button _joinbtn;
        public HostOrJoinScreen()
        {
            _hostbtn = new Button(new Rectangle(200, 300, 600, 150), Color.GREEN, 90, "HOST GAME");
            _joinbtn = new Button(new Rectangle(200, 500, 600, 150), Color.GREEN, 90, "JOIN GAME");
        }
        public override void Draw()
        {
            _hostbtn.Draw();
            _joinbtn.Draw();
        }

        public override void Update()
        {
            _hostbtn.Update();
            _joinbtn.Update();

            _hostbtn.OnAction += delegate
            {
                Console.WriteLine("HOST");
                ScreenManager.State = ScreenManager.ScreenState.PlayStateWithServer;
            };

            _joinbtn.OnAction += delegate
            {
                Console.WriteLine("JOIN");
                ScreenManager.State = ScreenManager.ScreenState.JoinState;
            };



        }
    }
}
