using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Networking;
using System.Numerics;
using System.Net;

namespace Checkers.Screens
{
    public class WaitForPlayerScreen : Screen
    {
        // Size of each frame, image in total has 5 x 21 frames.
        private const short _widthBean = 640;
        private const short _heightBean = 358;

        private string _ipAddress = string.Empty;

        // Frames
        private const int _framesSpeed = 15;
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
            _mrBean = Program.MrBeanSprite;
            _position = new Vector2((GetScreenWidth() / 2) - (_mrBean.width / (5 * 2)), (GetScreenHeight() / 2) - (_mrBean.height / (21 * 2)));
            _frameRec = new Rectangle(0.0f, 0.0f, _widthBean, _heightBean);

            new Thread(() =>
            {
                _ipAddress = GetIP();
            }).Start();

            Ready = true;
        }

        private string GetIP()
        {
            string ip = "NULL";
            try
            {
                IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                ip = ipEntry.AddressList[1].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ip;
        }
        public override void Draw()
        {
            DrawTextureRec(_mrBean, _frameRec, _position, Color.WHITE);
            DrawText("Waiting for opponent to join...", (int)_position.X, GetScreenHeight() - 50, 40, Color.WHITE);
            DrawText($"IP of server: {_ipAddress}, port: 1337", (int)_position.X -20, GetScreenHeight() - 150, 40, Color.MAROON);
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
                    _frameRec.y += (float)(_mrBean.height / 21);
                }

                if (_frameRec.y > (_mrBean.height - (_mrBean.height / 21)))
                {
                    _frameRec.y = 0;
                }

                _frameRec.x = (float)_currentFrame * (float)_mrBean.width / 5;
            }
            //Console.WriteLine($"frameCounter = {_framesCounter}, _currentFrame = {_currentFrame}, FrameRec.x = {_frameRec.x},\t\tFrameRec.y = {_frameRec.y}");

            if (Server.AmountOfPlayersActive == 2)
            {
                ScreenManager.State = ScreenManager.ScreenState.PlayState;
                UnloadTexture(_mrBean);
            }
        }
    }
}
