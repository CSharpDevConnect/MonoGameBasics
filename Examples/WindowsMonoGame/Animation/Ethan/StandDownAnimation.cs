using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoAnimation;

namespace WindowsMonoGame.Animation.Ethan
{
    public class StandDownAnimation : IAnimation
    {
        private readonly Frame _frame;

        private Texture2D _characterSheet;

        public StandDownAnimation()
        {
            _frame = new Frame(new Rectangle(0, 0, 30, 36), TimeSpan.FromSeconds(0.25));
        }

        public void LoadContent(ContentManager manager)
        {
            _characterSheet = manager.Load<Texture2D>("Ethan/Movement/EthanWalkingDownSpriteSheet");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_characterSheet, position, _frame.SourceRectangle, Color.White);
        }
    }
}
