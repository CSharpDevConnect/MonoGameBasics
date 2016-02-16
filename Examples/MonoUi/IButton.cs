using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoUi
{
    public interface IButton
    {
        Vector2 Position
        {
            get;
        }

        int Height
        {
            get;
        }

        int Width
        {
            get;
        }

        ButtonStatus Status
        {
            get;
        }

        void LoadContent(GraphicsDevice graphicsDevice);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
