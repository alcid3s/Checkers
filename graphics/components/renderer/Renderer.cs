using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components.renderer
{
    public abstract class Renderer : Component
    {
        public Renderer(GameObject parent) : base(parent) { }

        public override void Update() { }

        public void Draw()
        {
            Render(Parent.Transform);
        }

        protected abstract void Render(Transform transform);
    }
}
