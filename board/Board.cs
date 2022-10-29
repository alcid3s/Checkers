using Checkers.Networking;
using Checkers.pieces;
using Checkers.Screens;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace Checkers.board
{
    public class Board
    {
        // Used for when the server sends a message to the client.
        public bool GotReply { get; set; } = false;

        // bool and string are used so the server can send a FEN string to the client.
        public bool HasInitialised { get; private set; } = false;
        public string HasFen { get; set; } = string.Empty;

        // Used to create the board.
        public Tile[] Tiles { get; private set; } = new Tile[100];

        public PieceManager Manager { get; private set; }

        private const int sizeOfSquare = 96;

        private Piece.Side _sideOfPlayer;
        public SelectedPosition PositionSelected { get; set; }

        private readonly bool _isPlayer;

        public struct SelectedPosition
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
                    if (tile.Piece != null && tile.Piece.SideOfPiece.Equals(_sideOfPlayer) && Manager.LegalPieces().Contains(tile.Piece))
                    {
                        // The piece is selected.
                        PositionSelected = new(tile, tile.Piece);

                        // Will change color of all tiles on which the piece can move.
                        foreach (Tile t in Tiles)
                        {
                            t.ResetColor();
                        }

                        if (tile.Piece.CalculateForcingMoves().Count > 0)
                            tile.Piece.CalculateForcingMoves().ForEach(position =>
                            {
                                ScreenManager.Board.Tiles[position].Color = Color.VIOLET;
                            });
                        else
                            tile.Piece.CalculateRegularMoves().ForEach(position =>
                            {
                                Console.WriteLine(position);
                                ScreenManager.Board.Tiles[position].Color = Color.VIOLET;
                            });
                    }

                    // If the player already selected a position and presses on a tile without a piece on it.
                    else if (tile.Piece == null && PositionSelected.Tile != null && PositionSelected.Piece != null)
                    {
                        List<int> legalMoves = PositionSelected.Piece.CalculateRegularMoves();
                        if (legalMoves.Count == 0)
                            legalMoves = PositionSelected.Piece.CalculateRegularMoves();

                        // if the tile clicked is a legal move for the piece.
                        if (legalMoves.Contains(tile.GetPositionInTilesArray()))
                        {
                            new Thread(() =>
                            {
                                AwaitReplyFromServer(tile);
                            }).Start();

                            _ = SendToServer(tile);
                        }
                    }
                }
            }
        }

        // Is public because server also needs to run this on another thread.
        public void AwaitReplyFromServer(Tile tile)
        {
            while (!GotReply) ;

            if (GotReply)
            {
                GotReply = false;
                ChangePosition(tile);

                if (!_isPlayer)
                    Console.WriteLine($"SERVER: Changed position: {tile.GetPositionInTilesArray()}");
                else
                    Console.WriteLine($"CLIENT: Changed position: {tile.GetPositionInTilesArray()}");
            }
        }

        // Public so Client.cs can make use of it as well.
        public void ChangePosition(Tile tile)
        {
            // They wont ever be null but it removes all errors :)
            if (PositionSelected.Piece != null && PositionSelected.Tile != null)
            {
                
                //Manager.Move(PositionSelected.Piece, tile.GetPositionInTilesArray());
                Tiles[tile.GetPositionInTilesArray()].Attach(PositionSelected.Piece);
                PositionSelected.Tile.Detach();

                PositionSelected = new(null, null);

                foreach (Tile t in Tiles)
                {
                    t.ResetColor();
                }
            }
        }

        private async Task SendToServer(Tile tile)
        {
            if (PositionSelected.Tile != null)
            {
                string message = $"{PositionSelected.Tile.GetPositionInTilesArray()}:{typeof(Piece)}:{tile.GetPositionInTilesArray()};";
                await Client.Send(message);
            }
        }

        public void Init(string fen)
        {
            Manager = new PieceManager(this);

            Console.WriteLine($"FEN: {fen}");
            if(_isPlayer)
                Console.WriteLine("---------------EOL OF SETUP CLIENT-------------------\n");
            else
                Console.WriteLine("---------------EOL OF SETUP SERVER-------------------\n");

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
                    else if (c == 'p' || c == 'P')
                    {
                        Piece piece;
                        // A substitute for the earlier used Dictionary.
                        if (c == 'p')
                            piece = new ManPiece(Piece.Side.Black);
                        else
                            piece = new ManPiece(Piece.Side.White);

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

            foreach (Tile tile in Tiles)
            {
                if (tile.Piece != null)
                {
                    tile.Piece.CurrentPosition = tile;
                }
            }

            HasInitialised = true;
        }

        // This method should only be used by the server to verify if the move is legal.
        public bool IsLegalMove(int currentPosition, string typeOfPiece, int futurePosition)
        {

            if (!_isPlayer)
            {
                Console.WriteLine($"SERVER: currentpos: {currentPosition} and containspiece = {Tiles[currentPosition].Piece != null}");

                // Check if the place selected contains a piece on the board the server holds
                if (Tiles[currentPosition].Piece != null)
                {
                    Console.WriteLine("jjj:" + Tiles[currentPosition].Piece.CurrentPosition == null);
                    List<int> legalMoves = Tiles[currentPosition].Piece.CalculateRegularMoves();

                    // If the legalMoves are correct according to the server return true
                    if (legalMoves.Contains(futurePosition))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        // Parses information the server sends, server also uses this to parse the message the client sends. For changes in message structure change this method.
        public (int, string, int) ParseMessage(string message)
        {
            string[] data = message.Split(':');

            int currentPosition = int.Parse(data[0]);

            data[2] = data[2].Replace(';', ' ');
            data[2] = data[2].Trim();

            int futurePosition = int.Parse(data[2]);
            string typeOfPiece = data[1];

            return (currentPosition, typeOfPiece, futurePosition);
        }
    }
}
