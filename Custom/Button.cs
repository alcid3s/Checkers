using Checkers.graphics;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Checkers.Custom
{
    public delegate void OnAction();
    public class Button : INode
    {
        public OnAction OnAction { get; set; }
        private Rectangle _rectangle;
        private Color _color;
        private string _text;
        private short _sizeText;

        public Button(Rectangle rectangle, Color color, short sizeText, string text)
        {
            _rectangle = rectangle;
            _color = color;
            _text = text;
            _sizeText = sizeText;
        }
        public void Draw()
        {
            DrawRectangleRec(_rectangle, _color);
            DrawText(_text, (int)(_rectangle.x + (_rectangle.width / 16)), (int)(_rectangle.y + (_sizeText / 3)), _sizeText, Color.BLACK);
        }

        public void Update()
        {
            if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Vector2 mousePosition = GetMousePosition();
                if (_rectangle.x < mousePosition.X && _rectangle.x + _rectangle.width > mousePosition.X
                    && _rectangle.y < mousePosition.Y && _rectangle.y + _rectangle.height > mousePosition.Y)
                {
                    OnAction();
                }
            }
        }
    }
}
