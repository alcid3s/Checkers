using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components
{
    public abstract class Component
    {
        public bool Enabled { get; set; } = true;

        protected GameObject Parent;

        public Transform Transform { get => Parent.Transform; }

        public Component(GameObject parent)
        {
            Parent = parent;
        }

        public abstract void Update();
    }
}
