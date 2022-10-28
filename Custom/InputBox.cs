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
    public class InputBox : INode
    {
        private char[] text = new char[15];
        private short letterCount = 0;
        private Rectangle _textBox;
        public InputBox()
        {
            Rectangle _textBox = new Rectangle(GetScreenWidth() / 2 - 100, 180, 225, 50);
        }
        public void Draw()
        {
            DrawRectangleLines((int)_textBox.x, (int)_textBox.y, (int)_textBox.width, (int)_textBox.height, Color.WHITE);
            DrawText(new String(text), (int)_textBox.x + 5, (int)_textBox.y + 8, 40, Color.MAROON);
            
        }

        public void Update()
        {
            int key = GetCharPressed();
            while (key > 0)
            {
                if (key >= 48 && key <= 57 || key == 46)
                {
                    text[letterCount] = (char)key;
                    letterCount++;
                }
            }
        }
    }
}
