using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff.States.DerivedStates
{
    internal class MenuState : IState
    {
        private Texture2D mainMenuBackground;
        public MenuState()
        {

        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content)
        {
            mainMenuBackground = content.Load<Texture2D>("UI/MainMenuBackground");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDeviceManager.GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();
            //spriteBatch.Draw(mainMenuBackground, new Rectangle(0, 0, GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
        }
    }
}
