using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExerciseTwo_SideScrollerAnswer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Ground _mainGround;
        private Texture2D _mainGroundTexture;

        private Character _character = new Character(new Vector2(0, 0));
        private Texture2D _characterTexture;

        private Ground _box;
        private Texture2D _boxTexture;

        private Ground _floatingBox;
        private Texture2D _floatingBoxTexture;

        private IEnumerable<Ground> _groundsToCheck;

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
            // Initialize the objects the character is able to collide with
            _mainGround = new Ground(new Vector2(0, 200), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 200));
            _box = new Ground(new Vector2(200, 150), new Vector2(50, 50));
            _floatingBox = new Ground(new Vector2(300, 80), new Vector2(50, 50));

            _groundsToCheck = new List<Ground>
            {
                _mainGround,
                _box,
                _floatingBox,
            };

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create 1x1 textures of specific colours
            _mainGroundTexture = new Texture2D(GraphicsDevice, 1, 1);
            _mainGroundTexture.SetData(new[] { Color.SaddleBrown });

            _boxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _boxTexture.SetData(new[] { Color.Purple });

            _floatingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _floatingBoxTexture.SetData(new[] { Color.Black });

            _characterTexture = new Texture2D(GraphicsDevice, 1, 1);
            _characterTexture.SetData(new[] { Color.Red });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Nothing to unload because this game is so simple
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

            // Let the character determine where they are supposed to go
            _character.Update(_groundsToCheck);

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

            // Draw the textures we defined about WITH the same Color
            spriteBatch.Draw(_mainGroundTexture, _mainGround.Bounds, Color.SaddleBrown);

            spriteBatch.Draw(_boxTexture, _box.Bounds, Color.Purple);

            spriteBatch.Draw(_floatingBoxTexture, _floatingBox.Bounds, Color.Black);

            spriteBatch.Draw(_characterTexture, _character.Rectangle, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
