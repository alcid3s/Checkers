using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components.interactable.mouse_event
{
    internal class MouseButtonPress : MouseEvent
    {
        public MouseButtonPress(OnAction onAction) : base(onAction) { }

        public override void Update()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && (Collider?.HasCollision() ?? false))
            {
                OnAction();
            }
        }
    }
}
