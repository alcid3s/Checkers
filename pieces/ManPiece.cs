using Checkers.board;
using Raylib_cs;
using static Raylib_cs.Raylib;


namespace Checkers.pieces
{
    public class ManPiece : Piece
    {
        public ManPiece(Side side) : base(side)
        {
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

            Texture = LoadTextureFromImage(image);
            UnloadImage(image);
        }

        public override List<int> CalculateForcingMoves()
        {
            if (CurrentPosition.Piece == null)
                return new List<int>();

            List<(Tile?, Tile?)> possibleDirections = new List<(Tile?, Tile?)>();

            if (SideOfPiece == Side.White)
            {
                possibleDirections.Add((CurrentPosition.GetNorthEast(), CurrentPosition.GetNorthEast()?.GetNorthEast()));
                possibleDirections.Add((CurrentPosition.GetNorthWest(), CurrentPosition.GetNorthWest()?.GetNorthWest()));
            }
            else
            {
                possibleDirections.Add((CurrentPosition.GetSouthEast(), CurrentPosition.GetSouthEast()?.GetSouthEast()));
                possibleDirections.Add((CurrentPosition.GetSouthWest(), CurrentPosition.GetSouthWest()?.GetSouthWest()));
            }

#pragma warning disable CS8629
            return possibleDirections.Where(x => x.Item2 != null && CheckCapture(x.Item1, x.Item2)).Select(x => (int)x.Item2.GetPositionInTilesArray()).ToList();
#pragma warning restore CS8629
        }

        public override List<int> CalculateRegularMoves()
        {
            if (CurrentPosition.Piece == null)
                return new List<int>();

            List<Tile?> possibleDirections = new List<Tile?>();

            if (SideOfPiece == Side.White)
            {
                possibleDirections.Add(CurrentPosition.GetNorthEast());
                possibleDirections.Add(CurrentPosition.GetNorthWest());
            }
            else
            {
                possibleDirections.Add(CurrentPosition.GetSouthEast());
                possibleDirections.Add(CurrentPosition.GetSouthWest());
            }

#pragma warning disable CS8602
            return possibleDirections.Where(x => x != null && CheckMove(x)).Select(x => x.GetPositionInTilesArray()).ToList();
#pragma warning restore CS8602
        }
    }
}
