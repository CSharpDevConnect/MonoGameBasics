using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExerciseOne_ScreenSaverAnswer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Create a variable to hold the object we're going to move around the screen
        private Texture2D _head;

        private Vector2 _position = new Vector2(0, 0);
        private Vector2 _velocity = new Vector2(5, 5);

        private const int _headHeight = 70;
        private const int _headWidth = 55;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the sprite that we're going to move around the screen
            _head = Content.Load<Texture2D>("TrumpHead");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // If the top left of the head, plus the height of the head, plus the velocity in the Y direction is greater than the height of the user's window
            // change the direction of the Y velocity.
            if ((_position.Y + _headHeight) + _velocity.Y > GraphicsDevice.Viewport.Height)
            {
                _velocity.Y = _velocity.Y * -1;
            }

            // If the top of the head plus the velocity in the Y direction is less than than the top of the vieweable window change the direction of the X velocity
            if (_position.Y + _velocity.Y < 0)
            {
                _velocity.Y = _velocity.Y * -1;
            }

            // If the top left of the head, plus the width of the head, plus the velocity in the X direction is greater than the width of the user's window
            // change the direction of the X velocity.
            if ((_position.X + _headWidth) + _velocity.X > GraphicsDevice.Viewport.Width)
            {
                _velocity.X = _velocity.X * -1;
            }

            // If the top of the head plus the velocity in the X direction is less than the left of the vieweable window change the direction of the X velocity
            if (_position.X + _velocity.X < 0)
            {
                _velocity.X = _velocity.X * -1;
            }

            // when performing certain actions, like movement, it's important to take into account how long ago the last Update() was called
            // This is because not all CPUs and GPUs are made equal. If your game is doing a lot of heavy calculations or drawing some people
            // could have the game go really fast becuase they have a good CPU and you don't. Or in the opposite direciton, people see the game
            // going really slow becuase you have a fast CPU and they don't.
            // http://stackoverflow.com/questions/21735485/when-should-i-multiply-a-value-changing-over-time-per-gametime-elapsedgametime-t
            double elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds / 50;

            _position.X += (float)(_velocity.X * elapsedTime);
            _position.Y += (float)(_velocity.Y * elapsedTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw the head with the desired dimentions, at the current position
            spriteBatch.Draw(_head, new Rectangle((int)_position.X, (int)_position.Y, _headWidth, _headHeight), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
