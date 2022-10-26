using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using Checkers.graphics;

namespace Checkers.board
{
    public class Tile : GameObject
    {
        private Vector2 _positionOnBoard;
        private Rectangle _rectangle;
        private int _size;
        private bool _dark;

        private Piece? _piece;

        public Tile(int x, int y, int size, bool dark)
        {
            _size = size;
            _rectangle = new(x, y, x + size, y + size);

            _positionOnBoard = new Vector2(x / size, y / size);
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
            if(_dark)
                DrawRectangleRec(_rectangle, Color.DARKBROWN);
            else
                DrawRectangleRec(_rectangle, Color.BROWN);

            if (_piece != null)
            {
                DrawTexture(_piece.Texture, (int) _rectangle.x, (int) _rectangle.y, WHITE);
            }
        }

        public bool OnClick(Vector2 position)
        {
            //TODO 03-10-2022: simplify by one line return, for now keep console.writeline cuz its easy to identify which tile has been clicked.
            if (_rectangle.y < position.Y && position.Y < _rectangle.y + _size && _rectangle.x < position.X && position.X < _rectangle.x + _size)
            {
                Console.WriteLine($"X: {_positionOnBoard.X}, Y: {_positionOnBoard.Y}");
                return true;
            }
            else
                return false;
        }

    }
}
