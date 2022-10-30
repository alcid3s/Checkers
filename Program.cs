using Checkers.board;
using Checkers.Custom;
using Checkers.graphics;
using Checkers.pieces;
using Checkers.Screens;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.TraceLogLevel;

namespace Checkers
{
    public class Program
    {
        public static Texture2D MrBeanSprite;
        public static Texture2D WonSprite;
        public static Texture2D LostSprite;
        public static void Main()
        {
            SetTraceLogLevel(LOG_NONE);

            const int screenWidth = 960;
            const int screenHeight = 960;
            InitWindow(screenWidth, screenHeight, "Checkers");

            MrBeanSprite = LoadTexture("../../../res/MrBeanSprite.png");
            WonSprite = LoadTexture("../../../res/WonSprite.png");
            LostSprite = LoadTexture("../../../res/LostSprite.png");

            Piece.GetTextures();

            SetTraceLogLevel(LOG_NONE);

            ScreenManager manager = new ScreenManager(Color.LIME);

            SetTargetFPS(60);
            while (!WindowShouldClose())
            {
                BeginDrawing();
                manager.Update();
                manager.Draw();
                EndDrawing();
            }

            CloseWindow();
        }
    }
}