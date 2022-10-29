using Checkers.Custom;
using Checkers.graphics;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Screens
{
    internal class MainScreen : Screen
    {
        private Button _playbtn;
        public MainScreen(Color backGround)
        {
            _playbtn = new Button(new Rectangle(200,300,600,150), Color.GREEN, 90, "PLAY GAME");
        }

        public override void Draw()
        {
            _playbtn.Draw();
        }

        public override void Update()
        {
            _playbtn.Update();
            _playbtn.OnAction += delegate
            {
                ScreenManager.State = ScreenManager.ScreenState.HostOrJoinState;
            };
        }
    }
}
