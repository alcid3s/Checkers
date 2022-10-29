using Checkers.graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Custom;
using Checkers.Screens;

namespace Checkers.Custom
{
    public class InputBox : INode
    {
        public char[] Text = new char[15];

        public short Index { get; private set; } = 0 ; 

        private short _textSize;
        public Rectangle TextBox;

        public bool MouseOntextBox = false; 
        public InputBox(Rectangle rec, short textSize)
        {
            TextBox = new Rectangle(rec.x, rec.y, rec.width, rec.height);
            _textSize = textSize;
        }
        public void Draw()
        {
            DrawRectangleRec(TextBox, Color.LIGHTGRAY);
            DrawText(new string(Text), (int)TextBox.x + 5, (int)TextBox.y + 8, _textSize, Color.MAROON);
        }

        public void Update()
        {
            if (CheckCollisionPointRec(GetMousePosition(), TextBox))
                MouseOntextBox = true;
            else
                MouseOntextBox = false;

            if (MouseOntextBox)
            {
                int key = GetCharPressed();

                while (key > 0)
                {
                    if ((key >= 32) && (key <= 125) && (Index < Text.Length))
                    {
                        Text[Index] = (char)key;
                        Index++;
                    }

                    key = GetCharPressed();
                }

                if (IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                {
                    Index--;
                    if (Index < 0)
                    {
                        Index = 0;
                    }
                    Text[Index] = '\0';
                }
            }
        }
    }
}


