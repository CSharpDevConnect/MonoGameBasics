using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoAnimation.TwoDimensions
{
    public class BirdsEyeViewWalkingAnimationController : IAnimationController
    {
        private readonly IAnimation _standLeftAnimation;
        private readonly IAnimation _standRightAnimation;
        private readonly IAnimation _standUpAnimation;
        private readonly IAnimation _standDownAnimation;

        private readonly IAnimation _walkLeftAnimation;
        private readonly IAnimation _walkRightAnimation;
        private readonly IAnimation _walkUpAnimation;
        private readonly IAnimation _walkDownAnimation;

        private IAnimation _currentAnimation;

        public BirdsEyeViewWalkingAnimationController(IAnimation standLeftAnimation,
                                          IAnimation standRightAnimation,
                                          IAnimation standUpAnimation,
                                          IAnimation standDownAnimation,
                                          IAnimation walkLeftAnimation,
                                          IAnimation walkRightAnimation,
                                          IAnimation walkUpAnimation,
                                          IAnimation walkDownAnimation)
        {
            _standLeftAnimation = standLeftAnimation;
            _standRightAnimation = standRightAnimation;
            _standUpAnimation = standUpAnimation;
            _standDownAnimation = standDownAnimation;

            _walkLeftAnimation = walkLeftAnimation;
            _walkRightAnimation = walkRightAnimation;
            _walkUpAnimation = walkUpAnimation;
            _walkDownAnimation = walkDownAnimation;

            _currentAnimation = _standDownAnimation;
        }

        public void Update(GameTime gameTime, Vector2 velocity)
        {
            _currentAnimation = velocity == Vector2.Zero
                                    ? DetermineStandingDirection(_currentAnimation)
                                    : DetermineWalkingDirection(velocity);

            _currentAnimation.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            _currentAnimation.Draw(spriteBatch, position);
        }

        private IAnimation DetermineStandingDirection(IAnimation currentAnimation)
        {
            if (currentAnimation == _walkDownAnimation)
            {
                return _standDownAnimation;
            }
            else if (currentAnimation == _walkLeftAnimation)
            {
                return _standLeftAnimation;
            }
            else if (currentAnimation == _walkRightAnimation)
            {
                return _standRightAnimation;
            }
            else if (currentAnimation == _walkUpAnimation)
            {
                return _standUpAnimation;
            }
            else
            {
                return currentAnimation;
            }
        }

        private IAnimation DetermineWalkingDirection(Vector2 velocity)
        {
            bool movingHorizontally = Math.Abs(velocity.X) > Math.Abs(velocity.Y);
            if (movingHorizontally)
            {
                return velocity.X > 0
                           ? _walkRightAnimation
                           : _walkLeftAnimation;
            }
            else
            {
                return velocity.Y > 0
                           ? _walkDownAnimation
                           : _walkUpAnimation;
            }
        }
    }
}
