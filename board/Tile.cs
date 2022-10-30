using Checkers.graphics;
using Checkers.pieces;
using Checkers.Screens;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using static Raylib_cs.Raylib;

namespace Checkers.board
{
    public class Tile : GameObject
    {
        public Color Color;
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
            ResetColor();
        }

        public void ResetColor()
        {
            if (_dark)
                Color = new Color(0x70, 0x50, 0x40, 0xFF);
            else
                Color = new Color(0xC0, 0xB0, 0x80, 0xFF);
        }

        public void Attach(Piece piece)
        {
            Piece = piece;
            piece.CurrentPosition = this;
        }

        public Piece Detach()
        {
            Piece piece = this.Piece;
            this.Piece = null;
            return piece;
        }

        public new void Draw()
        {
            DrawRectangleRec(_rectangle, Color);

            if (Piece != null)
            {
                DrawTexture(Piece.Texture, (int)_rectangle.x, (int)_rectangle.y, Color.WHITE);
            }
        }

        public bool OnClick(Vector2 position)
        {
            return (_rectangle.y < position.Y && position.Y < _rectangle.y + _size && _rectangle.x < position.X && position.X < _rectangle.x + _size);
        }

        // Methods used for navigating board.
        public int GetPositionInTilesArray()
        {
            return (int)(PositionOnBoard.X + (PositionOnBoard.Y * 10));
        }

        public Tile? GetNorthWest()
        {
            int bounds = GetPositionInTilesArray() - 11;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? ScreenManager.Board.Tiles[bounds] : null;
        }

        public Tile? GetNorthEast()
        {
            int bounds = GetPositionInTilesArray() - 9;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? ScreenManager.Board.Tiles[bounds] : null;
        }

        public Tile? GetSouthEast()
        {
            int bounds = GetPositionInTilesArray() + 11;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? ScreenManager.Board.Tiles[bounds] : null;
        }

        public Tile? GetSouthWest()
        {
            int bounds = GetPositionInTilesArray() + 9;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? ScreenManager.Board.Tiles[bounds] : null;
        }

        // This method will make sure the piece cant move to the other side of the board. 
        private bool CheckTile(int bounds)
        {
            int currentPosition = GetPositionInTilesArray();

            // If the currentposition is on the eastern side and the bounds is on the western side or the other way around.
            return (bounds % 10 == 0 && (currentPosition - 9) % 10 == 0
                || (bounds - 9) % 10 == 0 && currentPosition % 10 == 0);
        }
    }
}
