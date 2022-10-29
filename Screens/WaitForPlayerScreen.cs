using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Networking;
using System.Numerics;

namespace Checkers.Screens
{
    public class WaitForPlayerScreen : Screen
    {
        // Size of each frame, image in total has 5 x 21 frames.
        private const short _widthBean = 640;
        private const short _heightBean = 358;

        // Frames
        private const int _framesSpeed = 30;
        private int _framesCounter = 0;
        private int _currentFrame = 0;

        // sprites
        private Texture2D _mrBean;

        // Positions
        private Vector2 _position;
        private Rectangle _frameRec;

        // References so outside methods can check when a sprite is finished loading.
        public bool Ready { get; private set; } = false;
        public WaitForPlayerScreen()
        {
            _mrBean = LoadTexture("../../../res/MrBeanSprite.png");
            //_position = new Vector2((GetScreenWidth() / 2) - (_mrBean.width / (5 * 2)), (GetScreenHeight() / 2) - (_mrBean.height / (21 * 2)));
            _position = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);
            _frameRec = new Rectangle(0.0f, 0.0f, (float)_mrBean.width / 5, (float)_mrBean.height / 21);
            Ready = true;
        }
        public override void Draw()
        {
            DrawTexture(_mrBean, 15, 40, Color.WHITE);
            DrawTextureRec(_mrBean, _frameRec, _position, Color.WHITE);
        }

        public override void Update()
        {
            if (Ready && _framesCounter >= (5 * 21))
            {
                _framesCounter = 0;
                _currentFrame = 0;
            }

            _framesCounter++;

            if (_framesCounter >= (60 / _framesSpeed))
            {
                _framesCounter = 0;
                _currentFrame++;

                if (_currentFrame > 4)
                {
                    _currentFrame = 0;
                    _frameRec.y += (_mrBean.height / 21);
                }

                if(_frameRec.y > (_mrBean.height - (_mrBean.height / 21)))
                {
                    _frameRec.y = 0;
                }

                _frameRec.x = (float)_currentFrame * (float)_mrBean.width / 5;
                Console.WriteLine($"FrameRec.x = {_frameRec.x}, FrameRec.y = {_frameRec.y}");
            }

            if (Server.AmountOfPlayersActive == 2)
            {
                ScreenManager.State = ScreenManager.ScreenState.PlayState;
                UnloadTexture(_mrBean);
            }
                
        }
    }
}
