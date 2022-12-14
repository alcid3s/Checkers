using Checkers.graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Custom
{

    public class LoseScreen : INode
    {
        private const short _width = 400;
        private const short _height = 400;

        private const int _frameSpeed = 20;
        private int _framesCounter = 0;
        private int _currentFrame = 0;

        // sprite.
        private Texture2D _lostSprite;

        //Positions
        private Vector2 _position;
        private Rectangle _frameRec;

        // display of screen.
        private Rectangle _box;

        private byte y = 0;

        private Button _exitBtn;
        public LoseScreen()
        {
            _lostSprite = Program.LostSprite;
            _box = new Rectangle((GetScreenWidth() / 2) - 250, (GetScreenHeight() / 2) - 250, 500, 500);

            _position = new Vector2(_box.x + (_box.width / 6), (_box.y + 40));
            _frameRec = new Rectangle(0.0f, 0.0f, _width, _height);

            _exitBtn = new Button(new Rectangle(_box.x + (_box.width / 4), _box.y + (_box.height - 60), 250, 50), Color.GREEN, 40, "EXIT GAME");
        }
        public void Draw()
        {
            DrawRectangleRec(_box, Color.DARKGRAY);

            DrawText("YOU LOST!", (int)(_box.x + (_box.width / 4)), (int)_box.y, 40, Color.RED);
            DrawTextureRec(_lostSprite, _frameRec, _position, Color.WHITE);

            _exitBtn.Draw();
        }

        public void Update()
        {
            _framesCounter++;

            if (_framesCounter >= (60 / _frameSpeed))
            {
                _framesCounter = 0;
                _currentFrame++;

                if (_currentFrame > 4)
                {
                    _currentFrame = 0;
                    y++;
                    _frameRec.y += (float)(_lostSprite.height / 4);
                }

                if (_frameRec.y > (_lostSprite.height - (_lostSprite.height / 4)) || y == 3 && _currentFrame == 2)
                {
                    _frameRec.y = 0;
                    _currentFrame = 0;
                    y = 0;
                }

                _frameRec.x = (float)_currentFrame * (float)_lostSprite.width / 5;
            }

            _exitBtn.Update();

            _exitBtn.OnAction += delegate
            {
                Program.UnloadTextures();
                Environment.Exit(0);
            };
        }
    }
}
