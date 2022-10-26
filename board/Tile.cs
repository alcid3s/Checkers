using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using Checkers.graphics;

namespace Checkers.board
{
    public class Tile : GameObject
    {
        public Vector2 PositionOnBoard;
        private Rectangle _rectangle;
        private int _size;
        private bool _dark;

        private Piece? _piece;

        public Tile(int x, int y, int size, bool dark)
        {
            _size = size;
            _rectangle = new(x, y, x + size, y + size);

            PositionOnBoard = new Vector2(x / size, y / size);
            _dark = dark;
        }

        public void Attach(Piece piece)
        {
            this._piece = piece;
        }

        public void Detach(Piece piece)
        {
            this._piece = null;
        }

        public void Draw()
        {
            if (_dark)
                DrawRectangleRec(_rectangle, Color.DARKBROWN);
            else
                DrawRectangleRec(_rectangle, Color.BROWN);

            if (_piece != null)
            {
                DrawTexture(_piece.Texture, (int)_rectangle.x, (int)_rectangle.y, WHITE);
            }
        }

        public bool OnClick(Vector2 mousePosition)
        {
            if (_rectangle.y < mousePosition.Y && mousePosition.Y < _rectangle.y + _rectangle.height && _rectangle.x < mousePosition.X && mousePosition.X < _rectangle.x + _rectangle.width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
