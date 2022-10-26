using Checkers.graphics;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Screens
{
    public abstract class Screen : INode
    { 
        public abstract void Draw();
        public abstract void Update();
    }
}
