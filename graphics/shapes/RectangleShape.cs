using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.shapes
{
    internal class RectangleShape : IShape
    {
        public void Draw(Transform transform, Color color)
        {
            Vector2 pos = transform.GlobalPosition() - transform.GlobalScale() / 2;
            Vector2 scale = transform.GlobalScale();
            Raylib.DrawRectangle((int)Math.Round(pos.X), (int)Math.Round(pos.Y), (int)Math.Round(scale.X), (int)Math.Round(scale.Y), color);
        }

        public bool InBounds(Transform transform, Vector2 point)
        {
            Vector2 pos1 = transform.GlobalPosition() - transform.GlobalScale() / 2;
            Vector2 pos2 = transform.GlobalPosition() + transform.GlobalScale() / 2;
            return point.X > pos1.X && point.X < pos2.X && point.Y > pos1.Y && point.Y < pos2.Y;
        }
    }
}
