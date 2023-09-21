using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff.States
{
    internal class MenuState : BaseState
    {
        private Texture2D mainMenuBackground;
        public MenuState(GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDeviceManager)
        { 
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            mainMenuBackground = content.Load<Texture2D>("UI/MainMenuBackground");
            //throw new System.NotImplementedException();
        }

        /*public override void UnloadContent()
        {
            throw new System.NotImplementedException();
        }*/

        public override void Update(GameTime gameTime)
        {
            //throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDeviceManager.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            // Draw sprites here
            spriteBatch.Draw(mainMenuBackground, new Rectangle(0, 0, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
            // throw new System.NotImplementedException();
        }
    }
}
