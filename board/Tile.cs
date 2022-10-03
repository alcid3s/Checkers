using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
namespace Checkers.board
{
    public class Tile
    {
        private Texture2D _texture;
        private Vector2 _positionOnBoard;
        private int _x, _y;
        private bool _dark;

        private Piece? _piece;

        public Tile(Texture2D texture, int x, int y, bool dark)
        {
            _texture = texture;
            _x = x;
            _y = y;

            _positionOnBoard = new Vector2(x / texture.width, y / texture.height);
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
            DrawTexture(_texture, _x, _y, WHITE);
            if(_piece != null)
            {
                DrawTexture(_piece.Texture, _x, _y, WHITE);
            }
        }

        public void OnClick()
        {
            Console.WriteLine($"X: {_positionOnBoard.X}, Y: {_positionOnBoard.Y}");
        }

    }
}
