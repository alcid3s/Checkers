using Checkers.graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Custom
{
    public class Checkbox : INode
    {
        public Rectangle Box;
        private Color _color;
        public bool State { get; private set; }
        public Checkbox(Rectangle rectangle, Color color, bool state)
        {
            Box = rectangle;
            _color = color;
            State = state;
        }
        public void Draw()
        {
            DrawRectangleRec(Box, _color);
            DrawRectangleLines((int)Box.x, (int)Box.y, (int)Box.width, (int)Box.height, Color.BLACK);

            if (State)
            {
                DrawLine((int)Box.x, (int)Box.y, (int)(Box.x + Box.width), (int)(Box.y + Box.height), Color.BLACK);
                DrawLine((int)Box.x, (int)(Box.y + Box.height), (int)(Box.x + Box.width), (int)Box.y, Color.BLACK);
            }
        }

        public void Update()
        {
            if (CheckCollisionPointRec(GetMousePosition(), Box))
            {
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_POINTING_HAND);
                if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    State = !State;
                    Console.Write($"Going");
                    if(State)
                        Console.WriteLine(" to save data.");
                    else
                        Console.WriteLine(" not to save data");
                }
            }
            else
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_DEFAULT);
        }
    }
}
