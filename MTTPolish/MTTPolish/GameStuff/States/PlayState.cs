using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff.States
{
    internal class PlayState : BaseState
    {
        public PlayState(GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDeviceManager)
        {
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            //throw new System.NotImplementedException();
        }

        /*public override void UnloadContent()
        {
            throw new System.NotImplementedException();
        }*/

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // Draw sprites here

            spriteBatch.End();
            // throw new System.NotImplementedException();
        }
    }
}
