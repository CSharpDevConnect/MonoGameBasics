using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoCharacter;
using MonoAnimation;

namespace WindowsMonoGame.Characters
{
    public class KeyboardControlledPlayableCharacter : ICharacter
    {
        private const int MOVE_DISTANCE = 2;

        private readonly IAnimationController _animationController;
        private Vector2 _position;

        public KeyboardControlledPlayableCharacter(IAnimationController animationController)
        {
            _animationController = animationController;
        }

        public void Initialize()
        {
            _position = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            Vector2 velocity = Vector2.Zero;

            // Move our sprite based on arrow keys being pressed:
            if (state.IsKeyDown(Keys.Right))
                velocity.X += MOVE_DISTANCE;
            if (state.IsKeyDown(Keys.Left))
                velocity.X -= MOVE_DISTANCE;
            if (state.IsKeyDown(Keys.Up))
                velocity.Y -= MOVE_DISTANCE;
            if (state.IsKeyDown(Keys.Down))
                velocity.Y += MOVE_DISTANCE;

            _animationController.Update(gameTime, velocity);
            _position += velocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationController.Draw(gameTime, spriteBatch, _position);
        }
    }
}
