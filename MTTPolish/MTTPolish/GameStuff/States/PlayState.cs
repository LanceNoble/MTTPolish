using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MTTPolish.GameStuff.States
{
    internal class PlayState : IState
    {
        Random randomNumberGenerator;
        Board level;
        public PlayState()
        {
            randomNumberGenerator = new Random();
            level = new Board(randomNumberGenerator, 30, 30, 0, 0, 0, 0);
        }

        public void Initialize()
        {
            level.Generate();
            //level.Reset();
            //level.Walk(new());
            level.Print();
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {
            //throw new System.NotImplementedException();
            //MouseState ms = Mouse.GetState();
            //if (ms.LeftButton.Equals(ButtonState.Pressed))
            //    level.Print();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDeviceManager.GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin();
            //spriteBatch.Draw(mainMenuBackground, new Rectangle(0, 0, GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
        }
    }
}
