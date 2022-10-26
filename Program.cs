using Checkers.board;
using Checkers.Custom;
using Checkers.graphics;
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

            /*
            SetTraceLogLevel(LOG_NONE);

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
            */

            Scene scene = new Scene();

            GameObject obj = new Button(scene);
            obj.Transform = new graphics.Transform(obj, new System.Numerics.Vector2(200, 100), new System.Numerics.Vector2(150, 50));
            GameObject obj2 = new Button(scene);
            obj2.Transform = new graphics.Transform(obj2, new System.Numerics.Vector2(200, 200), new System.Numerics.Vector2(200, 50));
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.RAYWHITE);
                scene.Update();
                scene.Draw();
                EndDrawing();
            }
        }
    }
}