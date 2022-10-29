using Checkers.Custom;
using Checkers.graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
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
        private Checkbox _saveGame;

        public bool SaveData { get; private set; } = false;
        public MainScreen(Color backGround)
        {
            _playbtn = new Button(new Rectangle(200,300,600,150), Color.GREEN, 90, "PLAY GAME");
            _saveGame = new Checkbox(new Rectangle(((GetScreenWidth() / 2) - 20) - 150, 500, 40, 40), Color.DARKGRAY, false);
        }

        public override void Draw()
        {
            _playbtn.Draw();
            _saveGame.Draw();
            DrawText("Save data from game",(int) _saveGame.Box.x + 60, (int)_saveGame.Box.y, 40, Color.RED);
        }

        public override void Update()
        {
            _playbtn.Update();
            _saveGame.Update();

            SaveData = _saveGame.State;
            _playbtn.OnAction += delegate
            {
                ScreenManager.State = ScreenManager.ScreenState.HostOrJoinState;
            };
        }
    }
}
