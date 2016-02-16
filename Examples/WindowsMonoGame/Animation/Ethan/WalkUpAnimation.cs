using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoAnimation;

namespace WindowsMonoGame.Animation.Ethan
{
    public class WalkUpAnimation : IAnimation
    {
        private readonly IEnumerable<Frame> _frames;
        private readonly TimeSpan _duration;

        private TimeSpan _timeIntoAnimation;
        private Texture2D _characterSheet;

        public WalkUpAnimation()
        {
            _frames = new List<Frame>
            {
                new Frame(new Rectangle(0, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(30, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(60, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(90, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(120, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(150, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(180, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
                new Frame(new Rectangle(210, 0, 30, 36), TimeSpan.FromSeconds(0.25)),
            };

            foreach (Frame frame in _frames)
            {
                _duration += frame.Duration;
            }
        }

        public void LoadContent(ContentManager manager)
        {
            _characterSheet = manager.Load<Texture2D>("Ethan/Movement/EthanWalkingUpSpriteSheet");
        }

        public void Update(GameTime gameTime)
        {
            double secondsIntoAnimation = _timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;
            double remainder = secondsIntoAnimation % _duration.TotalSeconds;

            _timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_characterSheet, position, GetCurrentRectangle(), Color.White);
        }

        private Rectangle GetCurrentRectangle()
        {
            Frame currentFrame = null;

            // See if we can find the frame
            TimeSpan accumulatedTime = new TimeSpan();
            foreach (var frame in _frames)
            {
                if (accumulatedTime + frame.Duration >= _timeIntoAnimation)
                {
                    currentFrame = frame;
                    break;
                }
                else
                {
                    accumulatedTime += frame.Duration;
                }
            }

            // If no frame was found, then try the last frame, 
            // just in case timeIntoAnimation somehow exceeds Duration
            if (currentFrame == null)
            {
                currentFrame = _frames.LastOrDefault();
            }

            // If we found a frame, return its rectangle, otherwise
            // return an empty rectangle (one with no width or height)
            if (currentFrame != null)
            {
                return currentFrame.SourceRectangle;
            }
            else
            {
                return Rectangle.Empty;
            }
        }
    }
}
