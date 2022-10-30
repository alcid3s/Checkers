using Checkers.board;
using Checkers.Screens;
using Raylib_cs;
using static Checkers.pieces.Piece;
using static Raylib_cs.Raylib;

namespace Checkers.pieces
{
    public abstract class Piece
    {
        public static Texture2D TextureManWhite { get; set; }
        public static Texture2D TextureKingWhite { get; set; }
        public static Texture2D TextureManBlack { get; set; }
        public static Texture2D TextureKingBlack { get; set; }

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

        public static void GetTextures()
        {
            TextureManWhite = GetTexture(96, 0);
            TextureKingWhite = GetTexture(0, 0);
            TextureManBlack = GetTexture(0, 96);
            TextureKingBlack = GetTexture(96, 96);
        }

        private static Texture2D GetTexture(int x, int y)
        {
            Image image = LoadImage("../../../res/Pieces2.png");

            ImageCrop(ref image, new Rectangle(x, y, x + 96, y + 96));

            Texture2D texture = LoadTextureFromImage(image);
            UnloadImage(image);

            return texture;
        }
    }
}
