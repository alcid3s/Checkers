using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics
{
    internal class Transform
    {
        private GameObject _parent;
        public Vector2 LocalPosition { get; set; }
        public Vector2 LocalScale { get; set; }

        public Transform(GameObject parent)
        {
            _parent = parent;
            LocalPosition = new Vector2(0, 0);
            LocalScale = new Vector2(1, 1);
        }

        public Transform(GameObject parent, Vector2 localPosition) : this(parent)
        {
            LocalPosition = localPosition;
        }

        public Transform(GameObject parent, Vector2 localPosition, Vector2 localScale) : this(parent, localPosition)
        {
            LocalScale = localScale;
        }

        public Vector2 GlobalPosition()
        {
            Stack<Transform> transforms = TransformStack();
            Vector2 position = new Vector2();
            Vector2 scalar = new Vector2(1, 1);

            while (transforms.Count > 0)
            {
                Transform transform = transforms.Pop();
                position += transform.LocalPosition * scalar;
                scalar *= transform.LocalScale;
            }

            return position;
        }

        public Vector2 GlobalScale()
        {
            Stack<Transform> transforms = TransformStack();
            Vector2 scale = new Vector2(1, 1);

            while (transforms.Count > 0)
            {
                Transform transform = transforms.Pop();
                scale *= transform.LocalScale;
            }

            return scale;
        }

        private Stack<Transform> TransformStack()
        {
            Stack<Transform> transforms = new Stack<Transform>();
            GameObject gameObject = _parent;
            while (gameObject != null)
            {
                transforms.Push(gameObject.Transform);
            }
            return transforms;
        }
    }
}
