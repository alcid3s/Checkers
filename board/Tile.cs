﻿using Checkers.graphics;
using Checkers.Screens;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using static Raylib_cs.Raylib;

namespace Checkers.board
{
    public class Tile : GameObject
    {
        public Color Color;
        public Vector2 PositionOnBoard;
        private Rectangle _rectangle;
        private int _size;
        private bool _dark;

        public Piece? Piece { get; private set; }
         
        public Tile(int x, int y, int size, bool dark)
        {
            _size = size;
            _rectangle = new(x, y, x + size, y + size);

            PositionOnBoard = new Vector2(x / size, y / size);
            _dark = dark;
            ResetColor();
        }

        public void ResetColor()
        {
            if (_dark)
                Color = Color.DARKBROWN;
            else
                Color = Color.BROWN;
        }

        public void Attach(Piece piece)
        {
            this.Piece = piece;
        }

        public void Detach(Piece piece)
        {
            this.Piece = null;
        }

        public new void Draw()
        {
            DrawRectangleRec(_rectangle, Color);

            if (Piece != null)
            {
                DrawTexture(Piece.Texture, (int)_rectangle.x, (int)_rectangle.y, Color.WHITE);
            }
        }

        public bool OnClick(Vector2 position)
        {
            return (_rectangle.y < position.Y && position.Y < _rectangle.y + _size && _rectangle.x < position.X && position.X < _rectangle.x + _size);
        }

        // Methods used for navigating board.
        private int GetPositionInTilesArray()
        {
            return (int)(PositionOnBoard.X + (PositionOnBoard.Y * 10));
        }

        public int? GetNorthWest()
        {
            int bounds = GetPositionInTilesArray() - 11;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? bounds : null;

            //if(bounds >= 0 && bounds <= 99 && !CheckTile(bounds))
            //{
            //    return bounds;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public int? GetNorthEast()
        {
            int bounds = GetPositionInTilesArray() - 9;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? bounds : null;
        }

        public int? GetSouthEast()
        {
            int bounds = GetPositionInTilesArray() + 11;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? bounds : null;
        }

        public int? GetSouthWest()
        {
            int bounds = GetPositionInTilesArray() + 9;
            return bounds >= 0 && bounds <= 99 && !CheckTile(bounds) ? bounds : null;
        }

        // This method will make sure the piece cant move to the other side of the board. 
        private bool CheckTile(int bounds)
        {
            int currentPosition = GetPositionInTilesArray();

            // If the currentposition is on the eastern side and the bounds is on the western side or the other way around.
            return (bounds % 10 == 0 && (currentPosition - 9) % 10 == 0
                || (bounds - 9) % 10 == 0 && currentPosition % 10 == 0);
        }
    }
}
