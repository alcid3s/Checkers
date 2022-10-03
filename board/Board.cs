using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;

namespace Checkers.board
{
    public class Board
    {
        private Tile[] _tiles = new Tile[100];
        private bool _dark;

        private static Texture2D _whiteTile = LoadTexture("../../../res/Tile1.png");
        private static Texture2D _darkTile = LoadTexture("../../../res/Tile2.png");

        public Board()
        {
            _dark = false;
            for (int y = 0; y < 10; y++)
            {
                // So not every rank has the same color
                _dark = !_dark;

                for (int x = 0; x < 10; x++)
                {
                    // Console.WriteLine($"Count: {y * 10 + x}");
                    if (_dark)
                        _tiles[y * 10 + x] = new Tile(_whiteTile, _whiteTile.width * x, _whiteTile.height * y, _dark);
                    else
                        _tiles[y * 10 + x] = new Tile(_darkTile, _darkTile.width * x, _darkTile.height * y, _dark);

                    // So the next Tile has the opposite color
                    _dark = !_dark;
                }
            }
        }

        public void Draw()
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw();
            }
        }

        public void Init(string fen)
        {
            Console.WriteLine($"FEN: {fen}");
            var dict = new Dictionary<char, Piece>()
            {
                ['P'] = new Piece(Piece.Side.Red),
                ['p'] = new Piece(Piece.Side.Black)
            };

            int x = 0, y = 0;
            foreach (char c in fen)
            {
                if (c == '/')
                {
                    x = 0;
                    y++;
                }
                else if (char.IsDigit(c))
                {
                    x += (int)Char.GetNumericValue(c);
                }
                else
                {
                    Piece piece = dict[c];

                    // Gets tile on which a piece needs to spawn.
                    _tiles[(y * 10) + x].Attach(piece);
                    x++;
                }
            }
        }
    }
}
