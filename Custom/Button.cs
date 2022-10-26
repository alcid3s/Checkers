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
    public class Button : INode
    {
        private Rectangle _rectangle;

        public Button(Vector2 size, Vector2 position)
        {
            _rectangle = new Rectangle(position.X, position.Y, position.X + size.X, position.Y + size.Y);
        }
        public void Draw()
        {
            DrawRectangleRec(_rectangle, Color.BLUE);
        }

        public void Update()
        {
            if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Vector2 mousePosition = GetMousePosition();
                if(_rectangle.x < mousePosition.X && _rectangle.x + _rectangle.width > mousePosition.X 
                    && _rectangle.y < mousePosition.Y && _rectangle.y + _rectangle.height > mousePosition.Y)
                {
                    Console.WriteLine("Button clicked");
                }
            }
        }
    }
}
