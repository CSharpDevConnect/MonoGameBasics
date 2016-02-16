using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoAnimation
{
    // based on https://developer.xamarin.com/guides/cross-platform/game_development/monogame/introduction/part2/
    public interface IAnimation
    {
        void LoadContent(ContentManager manager);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
