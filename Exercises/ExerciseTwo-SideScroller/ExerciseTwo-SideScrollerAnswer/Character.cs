using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExerciseTwo_SideScrollerAnswer
{
    public class Character
    {
        private Vector2 _position;
        private Vector2 _dimensions = new Vector2(50, 50);

        public Rectangle Rectangle
        {
            get;
            private set;
        }

        public Character(Vector2 startingPosition)
        {
            _position = startingPosition;
        }

        public void Update(IEnumerable<Ground> groundToCheck)
        {
            Vector2 velocity = DetermineVelocity();

            bool moveDown;
            bool moveRight;
            bool moveLeft;
            bool moveUp;
            DetermineAllowedMovement(groundToCheck, velocity, out moveDown, out moveRight, out moveLeft, out moveUp);
            
            if (moveDown && velocity.Y > 0)
            {
                _position.Y += velocity.Y;
            }

            if (moveRight && velocity.X > 0)
            {
                _position.X += velocity.X;
            }

            if (moveLeft && velocity.X < 0)
            {
                _position.X += velocity.X;
            }

            if (moveUp && velocity.Y < 0)
            {
                _position.Y += velocity.Y;
            }

            Rectangle = new Rectangle(_position.ToPoint(), _dimensions.ToPoint());
        }

        private Vector2 DetermineVelocity()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 velocity = new Vector2(0, 0);

            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Left))
            {
                velocity.X = 0;
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                velocity.X = 2;
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                velocity.X = -2;
            }

            if (state.IsKeyDown(Keys.Space))
            {
                velocity.Y = -2;
            }
            else
            {
                velocity.Y = 1;
            }

            return velocity;
        }

        private void DetermineAllowedMovement(IEnumerable<Ground> groundToCheck, Vector2 velocity, out bool moveDown, out bool moveRight, out bool moveLeft, out bool moveUp)
        {
            moveDown = true;
            moveRight = true;
            moveLeft = true;
            moveUp = true;

            Vector2 expectedPosition = _position + velocity;
            Rectangle expectedRectangle = new Rectangle(expectedPosition.ToPoint(), _dimensions.ToPoint());
            foreach (Ground ground in groundToCheck)
            {
                if (expectedRectangle.Intersects(ground.Bounds))
                {
                    // We're moving down, check to see if the bottom of the expected rectangle will be past the top of the ground
                    // also check to make sure the rectangle's (not expected rectangle) right is past the left side of the ground
                    // also check to see if the rectangle's left is past the right side of the ground
                    // We do the above two checks to make sure the rectangle is within the right and left bounds of the ground and not to its sides
                    if (velocity.Y > 0 &&
                        expectedRectangle.Bottom > ground.Bounds.Top &&
                        Rectangle.Left < ground.Bounds.Right &&
                        Rectangle.Right > ground.Bounds.Left)
                    {
                        moveDown = false;
                    }
                    // We're moving up, check to see if the top of the expected rectangle will be past OR equal to the bottom of the ground
                    // We do equal to, for this check because moving up is by 2 pixels, while all other directions move by one pixel
                    // also check to make sure the rectangle's (not expected rectangle) right is past the left side of the ground
                    // also check to see if the rectangle's left is past the right side of the ground
                    // We do the above two checks to make sure the rectangle is within the right and left bounds of the ground and not to its sides
                    else if (velocity.Y < 0 &&
                        expectedRectangle.Top <= ground.Bounds.Bottom &&
                        Rectangle.Left < ground.Bounds.Right &&
                        Rectangle.Right > ground.Bounds.Left)
                    {
                        moveUp = false;
                    }

                    // We're moving right, check to see if the right of the expected rectangle will be past the left of the ground
                    // also check to make sure the rectangle's (not expected rectangle) top is past the bottom side of the ground
                    // also check to see if the rectangle's bottom is past the top of the ground
                    // We do the above two checks to make sure the rectangle is within the top and bottom bounds of the ground and on top or below the ground
                    if (velocity.X > 0 &&
                        expectedRectangle.Right > ground.Bounds.Left &&
                        Rectangle.Top < ground.Bounds.Bottom &&
                        Rectangle.Bottom > ground.Bounds.Top)
                    {
                        moveRight = false;
                    }
                    // We're moving left, check to see if the left of the expected rectangle will be past the right of the ground
                    // also check to make sure the rectangle's (not expected rectangle) top is past the bottom side of the ground
                    // also check to see if the rectangle's bottom is past the top of the ground
                    // We do the above two checks to make sure the rectangle is within the top and bottom bounds of the ground and on top or below the ground
                    else if (velocity.X < 0 &&
                        expectedRectangle.Left < ground.Bounds.Right &&
                        Rectangle.Top < ground.Bounds.Bottom &&
                        Rectangle.Bottom > ground.Bounds.Top)
                    {
                        moveLeft = false;
                    }
                }
            }
        }
    }
}
