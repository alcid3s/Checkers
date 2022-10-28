using Checkers.board;
using Checkers.Screens;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Checkers.pieces
{
    public abstract class Piece
    {
        public Texture2D Texture { get; protected set; }
        public Side SideOfPiece { get; private set; }
        public Tile CurrentPosition { get; set; }
        public enum Side
        {
            White,
            Black
        }
        public Piece(Side side)
        {
            SideOfPiece = side;
        }

        public Tile? Tile()
        {
            foreach(Tile tile in ScreenManager.Board.Tiles)
            {
                if (tile.Piece == this)
                    return tile;
            }
            return null;
        } 

        public abstract List<int> CalculateForcingMoves();

        public abstract List<int> CalculateRegularMoves();

        protected static bool CheckMove(Tile? target)
        {
            return target.Piece == null;
        }

        protected bool CheckCapture(Tile? victim, Tile? target)
        {
            return victim.Piece != null && victim.Piece.SideOfPiece != SideOfPiece && target.Piece == null;
        }
    }
}
