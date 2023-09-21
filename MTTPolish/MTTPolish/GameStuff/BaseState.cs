using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff
{
    internal abstract class BaseState
    {
        //protected GraphicsDevice graphicsDevice;
        protected GraphicsDeviceManager graphicsDeviceManager;
        public BaseState(/*GraphicsDevice graphicsDevice, */GraphicsDeviceManager graphicsDeviceManager) 
        {
            //this.graphicsDevice = graphicsDevice;
            this.graphicsDeviceManager = graphicsDeviceManager;
        }
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        //public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
