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
        public Piece.Side WhoseTurn { get; set; }
        public Piece? LastCapturer { get; set; } = null;

        public PieceManager(Board board)
        {
            _board = board;
            WhoseTurn = Piece.Side.White;
        }

        public List<Piece> LegalPieces()
        {
            List<Piece> pieces = new List<Piece>();
            foreach (Tile tile in _board.Tiles)
                if (tile.Piece?.SideOfPiece == WhoseTurn && tile.Piece.CalculateForcingMoves().Count > 0)
                    pieces.Add(tile.Piece);

            if (pieces.Count != 0)
                return pieces;

            foreach (Tile tile in _board.Tiles)
                if (tile.Piece?.SideOfPiece == WhoseTurn && tile.Piece.CalculateRegularMoves().Count > 0)
                    pieces.Add(tile.Piece);

            return pieces;
        }

        public bool Move(Piece piece, int position)
        {
            if (!LegalPieces().Contains(piece))
                return false;

            if (LastCapturer != null && LastCapturer != piece)
                return false;

            if (piece.CalculateForcingMoves().Count > 0)
            {
                if (!piece.CalculateForcingMoves().Contains(position))
                    return false;

                _board.Tiles[(position + piece.CurrentPosition.GetPositionInTilesArray()) / 2].Detach();
                _board.Tiles[position].Attach(piece.CurrentPosition.Detach());

                if (piece.CalculateForcingMoves().Count > 0)
                    LastCapturer = piece;
                else
                    ToggleTurns();

                if (piece.GetType() == typeof(ManPiece))
                    if (position < 10 || position >= 90)
                        piece.CurrentPosition.Attach(new KingPiece(piece.SideOfPiece));

                return true;
            }

            if (!piece.CalculateRegularMoves().Contains(position))
                return false;

            _board.Tiles[position].Attach(piece.CurrentPosition.Detach());
            ToggleTurns();

            if (piece.GetType() == typeof(ManPiece))
                if (position < 10 || position >= 90)
                    piece.CurrentPosition.Attach(new KingPiece(piece.SideOfPiece));

            return true;
        }

        private void ToggleTurns()
        {
            LastCapturer = null;

            if (WhoseTurn == Piece.Side.White)
                WhoseTurn = Piece.Side.Black;
            else if (WhoseTurn == Piece.Side.Black)
                WhoseTurn = Piece.Side.White;
            else
            {
                throw new Exception("We done goofed on the turn toggling");
            }
        }
    }
}
