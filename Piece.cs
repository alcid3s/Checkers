using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Checkers
{

    public class Piece
    {
        public Texture2D Texture { get; private set; }
        public enum Side
        {
            Red,
            Black
        }

        public Piece(Side side)
        {
            Image image = LoadImage("../../../res/Pieces.png");
            if (side == Side.Red)
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
    }
}
