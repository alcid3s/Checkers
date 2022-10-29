using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.shapes
{
    public interface IShape
    {
        void Draw(Transform transform, Color color);
        bool InBounds(Transform transform, Vector2 point);
    }
}
