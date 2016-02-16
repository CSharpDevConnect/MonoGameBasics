using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MonoAnimation
{
    // Logic taken from: https://developer.xamarin.com/guides/cross-platform/game_development/monogame/introduction/part2/
    public class Frame
    {
        public Frame(Rectangle rectangle, TimeSpan duration)
        {
            SourceRectangle = rectangle;
            Duration = duration;
        }

        public Rectangle SourceRectangle { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}
