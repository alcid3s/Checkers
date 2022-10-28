using Checkers.board;
using Checkers.Screens;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Checkers
{
    public class Piece
    {
        public Texture2D Texture { get; private set; }
        public Side SideOfPiece { get; private set; }
        public enum Side
        {
            White,
            Black
        }
        public Piece(Side side)
        {
            SideOfPiece = side;
            Image image = LoadImage("../../../res/Pieces.png");
            if (side == Side.White)
            {
                ImageCrop(ref image, new Rectangle(96, 0, 192, 96));
            }
            else if (side == Side.Black)
            {
                ImageCrop(ref image, new Rectangle(0, 96, 96, 192));
            }
            else
            {
                throw new Exception("Error, Side is null");
            }
            this.Texture = LoadTextureFromImage(image);
            UnloadImage(image);
        }

        public List<int> CalculateLegalMoves(Tile currentPosition)
        {
            List<int> legalMoves = new();

            int? northEast = currentPosition.GetNorthEast();
            if(northEast != null)
                if(ScreenManager.Board.Tiles[(int)northEast].Piece == null)
                    legalMoves.Add(northEast.Value);

            int? northWest = currentPosition.GetNorthWest();
            if (northWest != null)
                if (ScreenManager.Board.Tiles[(int)northWest].Piece == null)
                    legalMoves.Add(northWest.Value);

            return legalMoves;
        }
    }
}
