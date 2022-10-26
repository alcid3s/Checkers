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
        public HostOrJoinScreen()
        {
            _hostbtn = new Button(new Rectangle(200, 300, 600, 150), Color.GREEN, 90, "HOST GAME");
        }
        public override void Draw()
        {
            _hostbtn.Draw();
        }

        public override void Update()
        {
            _hostbtn.Draw();
            _hostbtn.OnAction += delegate
            {
                ScreenManager.State = ScreenManager.ScreenState.PlayState;
            };
        }
    }
}
