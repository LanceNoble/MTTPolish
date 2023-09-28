using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MTTPolish.GameStuff.Enemies;
using System;

namespace MTTPolish.GameStuff.States
{
    internal class PlayState : IState
    {
        Texture2D pathTexture;
        Texture2D goblinTexture;

        Random randomNumberGenerator;
        Board level;
        Goblin goblin;
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

            goblin = new Goblin(level.Path);
        }

        public void LoadContent(ContentManager content)
        {
            pathTexture = content.Load<Texture2D>("Tiles/pathTexture");
            goblinTexture = content.Load<Texture2D>("Enemies/square");
        }

        public void Update(GameTime gameTime)
        {
            goblin.Move();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < level.Path.Length; i++) 
                level.Path[i].Draw(spriteBatch, pathTexture);
            goblin.Draw(spriteBatch, goblinTexture);
        }
    }
}
