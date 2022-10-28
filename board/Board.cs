using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using Checkers.graphics;

namespace Checkers.board
{
    public class Board
    {
        public bool HasInitialised { get; private set; } = false;
        public string HasFen { get; set; } = string.Empty;

        private Tile[] _tiles = new Tile[100];
        private bool _dark;

        private const int sizeOfSquare = 96;

        public Board()
        {
            _dark = true;
            for (int y = 0; y < 10; y++)
            {
                // So not every rank has the same color
                _dark = !_dark;

                for (int x = 0; x < 10; x++)
                {
                    // Console.WriteLine($"Count: {y * 10 + x}");
                    if (_dark)
                        _tiles[y * 10 + x] = new Tile(x * sizeOfSquare, y * sizeOfSquare, sizeOfSquare, _dark);
                    else
                        _tiles[y * 10 + x] = new Tile(x * sizeOfSquare, y * sizeOfSquare, sizeOfSquare, _dark);

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

        public void Update()
        {
            if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                OnClick(GetMousePosition());
            }
        }

        private void OnClick(Vector2 position)
        {
            foreach(Tile tile in _tiles)
            {
                if (tile.OnClick(position))
                {
                    Console.WriteLine($"X: {tile.PositionOnBoard.X}, Y: {tile.PositionOnBoard.Y}");
                    if(tile.Piece != null)
                        Console.WriteLine("Contains piece");
                }
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
                else if (c.Equals(';'))
                    break;
                else
                {
                    Piece piece = dict[c];

                    // Gets tile on which a piece needs to spawn.
                    _tiles[(y * 10) + x].Attach(piece);
                    x++;
                }
            }
            HasInitialised = true;
        }
    }
}
