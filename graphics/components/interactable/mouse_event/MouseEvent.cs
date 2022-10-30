using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components.interactable.mouse_event
{
    public delegate void OnAction();

    public abstract class MouseEvent
    {
        public OnAction OnAction { get; set; }

        protected MouseCollider? Collider;

        public MouseEvent(OnAction onAction)
        {
            OnAction = onAction;
        }

        public void SetCollider(MouseCollider? collider)
        {
            Collider = collider;
        }

        public abstract void Update();
    }
}
