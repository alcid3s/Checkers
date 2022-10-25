using Checkers.graphics.components.interactable.mouse_event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.graphics.components.interactable
{
    public class Interactable : Component
    {
        public MouseCollider? Collider { private get; set; }

        private List<MouseEvent> _mouseEvents = new List<MouseEvent>();

        public Interactable(GameObject parent) : base(parent) { }

        public override void Update()
        {
            foreach (MouseEvent mouseEvent in _mouseEvents)
                mouseEvent.Update();
        }

        public void SetEvent(MouseEvent mouseEvent)
        {
            _mouseEvents.Add(mouseEvent);
        }
    }
}
