using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.graphics.components;

namespace Checkers.graphics
{
    internal class GameObject : INode
    {
        public GameObject? Parent { get; private set; }
        public Transform Transform { get; set; }

        protected List<GameObject> Children = new List<GameObject>();

        private List<Component> _components = new List<Component>();

        public GameObject()
        {
            Parent = null;
            Transform = new Transform(this);
        }

        public GameObject(GameObject parent)
        {
            Parent = parent;
            Transform = new Transform(this);
            parent.Children.Add(this);
        }

        public void Draw()
        {
            //Renderer needed
        }

        public void Update()
        {
            //Component needs an Update() function
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
        }

        public T[] GetComponents<T>() where T : Component
        {
            return _components.Where(x => x is T).Select(x => (T)x).ToArray();
        }

        public T GetComponent<T>() where T : Component
        {
            return GetComponents<T>()[0];
        }
    }
}
