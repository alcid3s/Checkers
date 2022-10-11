using Checkers.graphics.shapes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components.interactable
{
    internal class MouseCollider
    {
        private IShape _collider;
        private Interactable _parent;

        public MouseCollider(IShape shape, Interactable parent)
        {
            _collider = shape;
            _parent = parent;
        }

        public bool HasCollision()
        {
            return _collider.InBounds(_parent.Transform, Raylib.GetMousePosition());
        }
    }
}
