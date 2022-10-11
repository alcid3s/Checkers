using Checkers.graphics.shapes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components.renderer
{
    internal class ShapeRenderer : Renderer
    {
        public IShape Shape { get; set; }
        public Color Color { get; set; }

        public ShapeRenderer(GameObject parent, IShape shape) : base(parent)
        {
            Shape = shape;
            Color = new Color(0, 0, 0, 0);
        }

        protected override void Render(Transform transform)
        {
            Shape.Draw(Parent.Transform, Color);
        }
    }
}
