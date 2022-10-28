using Checkers.board;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Checkers.pieces
{
    public class KingPiece : Piece
    {
        public KingPiece(Side side) : base(side)
        {
            Image image = LoadImage("../../../res/Pieces.png");
            if (side == Side.White)
            {
                ImageCrop(ref image, new Rectangle(0, 0, 96, 96));
            }
            else if (side == Side.Black)
            {
                ImageCrop(ref image, new Rectangle(96, 96, 192, 192));
            }
            else
            {
                throw new Exception("Error, Side is null");
            }
            Texture = LoadTextureFromImage(image);
            UnloadImage(image);
        }

        public override List<int> CalculateForcingMoves()
        {
            Tile currentPosition = Tile();

            if (currentPosition.Piece == null)
                return new List<int>();

            List<(Tile?, Tile?)> possibleDirections = new List<(Tile?, Tile?)>();

            possibleDirections.Add((currentPosition.GetNorthEast(), currentPosition.GetNorthEast()?.GetNorthEast()));
            possibleDirections.Add((currentPosition.GetNorthWest(), currentPosition.GetNorthWest()?.GetNorthWest()));
            possibleDirections.Add((currentPosition.GetSouthEast(), currentPosition.GetSouthEast()?.GetSouthEast()));
            possibleDirections.Add((currentPosition.GetSouthWest(), currentPosition.GetSouthWest()?.GetSouthWest()));

#pragma warning disable CS8629
            return possibleDirections.Where(x => x.Item2 != null && CheckCapture(x.Item1, x.Item2)).Select(x => (int)x.Item2.GetPositionInTilesArray()).ToList();
#pragma warning restore CS8629
        }

        public override List<int> CalculateRegularMoves()
        {
            Tile currentPosition = Tile();

            if (currentPosition.Piece == null)
                return new List<int>();

            List<Tile?> possibleDirections = new List<Tile?>();

            possibleDirections.Add(currentPosition.GetNorthEast());
            possibleDirections.Add(currentPosition.GetNorthWest());
            possibleDirections.Add(currentPosition.GetSouthEast());
            possibleDirections.Add(currentPosition.GetSouthWest());

#pragma warning disable CS8602
            return possibleDirections.Where(x => x != null && CheckMove(x)).Select(x => x.GetPositionInTilesArray()).ToList();
#pragma warning restore CS8602
        }
    }
}
