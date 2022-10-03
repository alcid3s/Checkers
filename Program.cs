using Checkers.board;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.TraceLogLevel;

namespace Checkers
{
    internal class Program
    {
        public static string Setup = "1p1p1p1p1p/p1p1p1p1p1/1p1p1p1p1p/p1p1p1p1p1/10/10/1P1P1P1P1P/P1P1P1P1P1/1P1P1P1P1P/P1P1P1P1P1";
        static void Main(string[] args)
        {
            SetTraceLogLevel(LOG_NONE);

            const int screenWidth = 960;
            const int screenHeight = 960;
            InitWindow(screenWidth, screenHeight, "Checkers");

            Board board = new();
            board.Init(Setup);

            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.RAYWHITE);
                board.Update();
                board.Draw();
                EndDrawing();
            }
        }
    }
}