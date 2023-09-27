using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MTTPolish.GameStuff.States
{
    internal class PlayState : IState
    {
        Texture2D pathTexture;

        Random randomNumberGenerator;
        Board level;
        public PlayState()
        {
            randomNumberGenerator = new Random();

            // For now, best to keep the dimensions the same aspect ratio as the window
            level = new Board(randomNumberGenerator, 18, 32);
        }

        public void Initialize()
        {
            level.Generate();
            level.Print();
        }

        public void LoadContent(ContentManager content)
        {
            pathTexture = content.Load<Texture2D>("Tiles/pathTexture");
        }

        public void Update(GameTime gameTime)
        {
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDeviceManager.GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin();
            for (int i = 0; i < level.Path.Length; i++) 
                level.Path[i].Draw(spriteBatch, pathTexture);

            spriteBatch.End();
        }
    }
}
