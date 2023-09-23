using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff.States.DerivedStates
{
    internal class PauseState : IState
    {
        public PauseState()
        {

        }

        public void Initialize()
        {
            //throw new System.NotImplementedException();
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            //throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDeviceManager.GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            //spriteBatch.Draw(mainMenuBackground, new Rectangle(0, 0, GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
        }
    }
}
