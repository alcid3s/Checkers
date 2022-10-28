using Checkers.board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.pieces
{
    public class PieceManager
    {
        private Board _board;
        public Piece.Side WhoseTurn;

        public PieceManager(Board board)
        {
            _board = board;
            WhoseTurn = Piece.Side.White;
        }

        public List<Piece> LegalPieces()
        {
            List<Piece> pieces = new List<Piece>();
            foreach (Tile tile in _board.Tiles)
                if (tile.Piece?.SideOfPiece == WhoseTurn && tile.Piece.CalculateForcingMoves(tile).Count > 0)
                    pieces.Add(tile.Piece);

            if (pieces.Count != 0)
                return pieces;

            foreach (Tile tile in _board.Tiles)
                if (tile.Piece?.SideOfPiece == WhoseTurn && tile.Piece.CalculateRegularMoves(tile).Count > 0)
                    pieces.Add(tile.Piece);

            return pieces;
        }
    }
}
