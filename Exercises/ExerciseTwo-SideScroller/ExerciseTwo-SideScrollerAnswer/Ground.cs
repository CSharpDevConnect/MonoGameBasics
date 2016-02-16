using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ExerciseTwo_SideScrollerAnswer
{
    public class Ground
    {
        public Ground(Vector2 position, Vector2 size)
        {
            Bounds = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public Rectangle Bounds
        {
            get;
            private set;
        }
    }
}
