using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using Checkers.graphics;
using Checkers.Screens;
using Checkers.Networking;
using System.Text;

namespace Checkers.board
{
    public class Board
    {
        // bool and string are used so the server can send a FEN string to the client.
        public bool HasInitialised { get; private set; } = false;
        public string HasFen { get; set; } = string.Empty;

        // Used to create the board.
        public Tile[] Tiles { get; private set; } = new Tile[100];

        private const int sizeOfSquare = 96;

        private Piece.Side _sideOfPlayer;
        private SelectedPosition _selectedPosition;

        private bool _isPlayer;

        struct SelectedPosition
        {
            public Tile? Tile { get; set; }
            public Piece? Piece { get; set; }

            public SelectedPosition(Tile? tile, Piece? piece)
            {
                Tile = tile;
                Piece = piece;
            }
        }

        public Board(bool isPlayer)
        {
            bool dark = true;
            for (int y = 0; y < 10; y++)
            {
                // So not every rank has the same color
                dark = !dark;

                for (int x = 0; x < 10; x++)
                {
                    // Console.WriteLine($"Count: {y * 10 + x}");
                    if (dark)
                        Tiles[y * 10 + x] = new Tile(x * sizeOfSquare, y * sizeOfSquare, sizeOfSquare, dark);
                    else
                        Tiles[y * 10 + x] = new Tile(x * sizeOfSquare, y * sizeOfSquare, sizeOfSquare, dark);

                    // So the next Tile has the opposite color
                    dark = !dark;
                }
            }
            _isPlayer = isPlayer;
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

                    // if the tile contains a piece and the piece is of the same side as the player.
                    if (tile.Piece != null && tile.Piece.SideOfPiece.Equals(_sideOfPlayer))
                    {
                        // The piece is selected.
                        _selectedPosition = new(tile, tile.Piece);

                        // Will change color of all tiles on which the piece can move.
                        _selectedPosition.Piece.CalculateLegalMoves(tile).ForEach(position =>
                        {
                            ScreenManager.Board.Tiles[position].Color = Color.VIOLET;
                        });
                    }

                    // If the player already selected a position and presses on a tile without a piece on it.
                    else if(tile.Piece == null && _selectedPosition.Tile != null && _selectedPosition.Piece != null)
                    {
                        List<int> legalMoves = _selectedPosition.Piece.CalculateLegalMoves(_selectedPosition.Tile);

                        // if the tile clicked is a legal move for the piece.
                        if (legalMoves.Contains(tile.GetPositionInTilesArray()))
                        {
                            string message = _selectedPosition.Tile.GetPositionInTilesArray() + ":" + 
                                typeof(Piece).ToString() + ':' + tile.GetPositionInTilesArray() + ';';

                            Client.Send(message);
                        }
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

        // This method should only be used by the server to verify if the move is legal.
        public bool IsLegalMove(int currentPosition, string typeOfPiece, int futurePosition)
        {
            if (!_isPlayer)
            {
                // Check if the place selected contains a piece on the board the server holds
                if (Tiles[currentPosition].Piece != null)
                {
                    List<int> legalMoves = Tiles[currentPosition].Piece.CalculateLegalMoves(Tiles[currentPosition]);

                    // If the legalMoves are correct according to the server return true
                    if (legalMoves.Contains(futurePosition))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
