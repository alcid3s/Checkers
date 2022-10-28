using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using Checkers.graphics;
using Checkers.Screens;

namespace Checkers.board
{
    public class Board
    {
        // bool and string are used so the server can send a FEN string to the client.
        public bool HasInitialised { get; private set; } = false;
        public string HasFen { get; set; } = string.Empty;

        // Used to create the board.
        public Tile[] Tiles { get; private set; } = new Tile[100];
        private bool _dark;

        private const int sizeOfSquare = 96;

        private Piece? _selectedPiece = null;

        private Piece.Side _sideOfPlayer;

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
                        Tiles[y * 10 + x] = new Tile(x * sizeOfSquare, y * sizeOfSquare, sizeOfSquare, _dark);
                    else
                        Tiles[y * 10 + x] = new Tile(x * sizeOfSquare, y * sizeOfSquare, sizeOfSquare, _dark);

                    // So the next Tile has the opposite color
                    _dark = !_dark;
                }
            }
        }

        public void Draw()
        {
            foreach (Tile tile in Tiles)
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
            foreach (Tile tile in Tiles)
            {
                if (tile.OnClick(position))
                {
                    Console.WriteLine($"X: {tile.PositionOnBoard.X}, Y: {tile.PositionOnBoard.Y}");

                    // if the tile contains a piece and the piece is of the same side as the player
                    if (tile.Piece != null && tile.Piece.SideOfPiece.Equals(_sideOfPlayer))
                    {
                        Console.WriteLine($"side of piece: {tile.Piece.SideOfPiece.ToString()}, side of player: {_sideOfPlayer.ToString()}");
                        // The piece is selected.
                        _selectedPiece = tile.Piece;

                        // Will change color of all tiles on which the piece can move.
                        _selectedPiece.CalculateLegalMoves(tile).ForEach(position =>
                        {
                            ScreenManager.Board.Tiles[position].Color = Color.VIOLET;
                        });
                    }
                }
            }
        }

        public void Init(string fen)
        {
            Console.WriteLine($"FEN: {fen}");
            var dict = new Dictionary<char, Piece>()
            {
                ['P'] = new Piece(Piece.Side.White),
                ['p'] = new Piece(Piece.Side.Black)
            };

            int x = 0, y = 0;
            bool endOfFEN = false;
            foreach (char c in fen)
            {
                if (!endOfFEN)
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
                        endOfFEN = true;
                    else if(c == 'p' || c == 'P')
                    {
                        Piece piece = dict[c];

                        // Gets tile on which a piece needs to spawn.
                        Tiles[(y * 10) + x].Attach(piece);
                        x++;
                    }
                }
                else
                {
                    if (c == 'W')
                    {
                        _sideOfPlayer = Piece.Side.White;
                    }
                    else if (c == 'B')
                    {
                        _sideOfPlayer = Piece.Side.Black;
                    }
                }
            }
            Console.WriteLine("side: " + _sideOfPlayer.ToString());
            HasInitialised = true;
        }
    }
}
