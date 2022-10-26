using Checkers.graphics;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Checkers.graphics.shapes;
using Checkers.graphics.components.renderer;
using Checkers.graphics.components.interactable;
using Checkers.graphics.components.interactable.mouse_event;

namespace Checkers.Custom
{
    public class Button : GameObject
    {
        private IShape _shape;

        public Button(GameObject parent) : base(parent) 
        {
            _shape = new RectangleShape();
            ShapeRenderer renderer = new ShapeRenderer(this, _shape);
            renderer.Color = new Raylib_cs.Color(0x80, 0x60, 0x20, 0xFF);
            AddComponent(renderer);
            Interactable interactable = new Interactable(this);
            MouseCollider collider = new MouseCollider(_shape, interactable);
            interactable.Collider = collider;
            MouseEvent mEvent = new MouseButtonPress(() =>
            {
                Console.WriteLine("yay");
            });
            mEvent.SetCollider(collider);
            interactable.SetEvent(mEvent);
            AddComponent(interactable);
        }
    }
}
