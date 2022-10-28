using Checkers.graphics;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace Checkers.board
{
    public class Tile : GameObject
    {
        public Vector2 PositionOnBoard;
        private Rectangle _rectangle;
        private int _size;
        private bool _dark;

        public Piece? Piece { get; private set; }
         
        public Tile(int x, int y, int size, bool dark)
        {
            _size = size;
            _rectangle = new(x, y, x + size, y + size);

            PositionOnBoard = new Vector2(x / size, y / size);
            _dark = dark;
        }

        public void Attach(Piece piece)
        {
            this.Piece = piece;
        }

        public void Detach(Piece piece)
        {
            this.Piece = null;
        }

        public new void Draw()
        {
            if (_dark)
                DrawRectangleRec(_rectangle, Color.DARKBROWN);
            else
                DrawRectangleRec(_rectangle, Color.BROWN);

            if (Piece != null)
            {
                //DrawCircle((int)_rectangle.x, (int)_rectangle.y, 50, Color.WHITE);
                DrawTexture(Piece.Texture, (int)_rectangle.x, (int)_rectangle.y, Color.WHITE);
            }
        }

        public bool OnClick(Vector2 position)
        {
            return (_rectangle.y < position.Y && position.Y < _rectangle.y + _size && _rectangle.x < position.X && position.X < _rectangle.x + _size);
        }
    }
}
