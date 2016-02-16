using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoUi
{
    public class BasicButton : IButton
    {
        private readonly SpriteFont _font;
        private readonly Color _backgroundColour;
        private readonly Color _highlightedColour;
        private readonly Color _pressedColour;
        private readonly string _content;
        private readonly Vector2 _contentLocation;

        private Texture2D _rectangle;
        private Color _activeColor;
        private ButtonStatus _previousStatus;

        public BasicButton(Vector2 position, int height, int width, SpriteFont font, Color backgroundColour, Color highlightedColour, Color pressedColour, string content)
        {
            Position = position;
            Height = height;
            Width = width;
            _font = font;
            _backgroundColour = backgroundColour;
            _highlightedColour = highlightedColour;
            _activeColor = _backgroundColour;
            _pressedColour = pressedColour;

            _content = content;
            Vector2 contentSize = font.MeasureString(content);
            _contentLocation = new Vector2((Position.X + (Width / 2) - contentSize.X / 2), (Position.Y + (Height / 2)) - contentSize.Y / 2);

            Status = ButtonStatus.Idle;
        }

        public Vector2 Position
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        }

        public ButtonStatus Status
        {
            get;
            private set;
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            _rectangle = new Texture2D(graphicsDevice, 1, 1);
            _rectangle.SetData(new[] { _backgroundColour });
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            if (state.X >= Position.X && state.X <= Position.X + Width &&
                state.Y >= Position.Y && state.Y <= Position.Y + Height)
            {
                _activeColor = _highlightedColour;

                if (state.LeftButton == ButtonState.Pressed)
                {
                    Status = ButtonStatus.Clicked;
                    _previousStatus = Status;
                    _activeColor = _pressedColour;
                }
                else if (_previousStatus == ButtonStatus.Clicked &&
                         state.LeftButton == ButtonState.Released)
                {
                    Status = ButtonStatus.Released;
                    _previousStatus = ButtonStatus.Clicked;
                    _activeColor = _backgroundColour;
                }
            }
            else if (Status == ButtonStatus.Clicked &&
                     state.LeftButton == ButtonState.Pressed)
            {
                _activeColor = _pressedColour;
            }
            else
            {
                Status = ButtonStatus.Idle;
                _previousStatus = ButtonStatus.Idle;
                _activeColor = _backgroundColour;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_rectangle, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), _activeColor);
            spriteBatch.DrawString(_font, _content, _contentLocation, Color.Black);
        }
    }
}
