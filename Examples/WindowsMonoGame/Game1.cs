using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoAnimation;
using MonoAnimation.TwoDimensions;
using MonoCharacter;
using MonoUi;
using WindowsMonoGame.Characters;

namespace WindowsMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private static readonly FrameCounter _frameCounter = new FrameCounter();

        private readonly ICharacter _character;
        private readonly IAnimationController _playerAnimationController;
        private readonly GraphicsDeviceManager _graphics;
        private readonly IAnimation _standLeftAnimation = new Animation.Ethan.StandLeftAnimation();
        private readonly IAnimation _standRightAnimation = new Animation.Ethan.StandRightAnimation();
        private readonly IAnimation _standUpAnimation = new Animation.Ethan.StandUpAnimation();
        private readonly IAnimation _standDownAnimation = new Animation.Ethan.StandDownAnimation();
        private readonly IAnimation _walkLeftAnimation = new Animation.Ethan.WalkLeftAnimation();
        private readonly IAnimation _walkRightAnimation = new Animation.Ethan.WalkRightAnimation();
        private readonly IAnimation _walkUpAnimation = new Animation.Ethan.WalkUpAnimation();
        private readonly IAnimation _walkDownAnimation = new Animation.Ethan.WalkDownAnimation();

        private BasicButton _exitButton;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _playerAnimationController = new BirdsEyeViewWalkingAnimationController(_standLeftAnimation,
                                                                        _standRightAnimation,
                                                                        _standUpAnimation,
                                                                        _standDownAnimation,
                                                                        _walkLeftAnimation,
                                                                        _walkRightAnimation,
                                                                        _walkUpAnimation,
                                                                        _walkDownAnimation);

            _character = new KeyboardControlledPlayableCharacter(_playerAnimationController);

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("ButtonFont");

            _exitButton = new BasicButton(new Vector2(100, 100), 50, 100, _font, Color.White, Color.Yellow, Color.Green, "Exit");
            _exitButton.LoadContent(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _standDownAnimation.LoadContent(Content);
            _standLeftAnimation.LoadContent(Content);
            _standRightAnimation.LoadContent(Content);
            _standUpAnimation.LoadContent(Content);

            _walkDownAnimation.LoadContent(Content);
            _walkLeftAnimation.LoadContent(Content);
            _walkRightAnimation.LoadContent(Content);
            _walkUpAnimation.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _exitButton.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || _exitButton.Status == ButtonStatus.Released)
                Exit();

            _character.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _character.Draw(gameTime, _spriteBatch);
            _exitButton.Draw(_spriteBatch);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);

            string fps = string.Format("FPS: {0}\r\nX: {1}\r\nY: {2}", _frameCounter.AverageFramesPerSecond, Mouse.GetState().X, Mouse.GetState().Y);
            _spriteBatch.DrawString(_font, fps, new Vector2(1, 1), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
