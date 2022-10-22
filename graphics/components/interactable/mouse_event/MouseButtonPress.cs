using Raylib_cs;
using static Raylib_cs.Raylib;
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
            if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && (Collider?.HasCollision() ?? false))
            {
                OnAction();
            }
        }
    }
}
