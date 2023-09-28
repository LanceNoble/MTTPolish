using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MTTPolish.GameStuff.Enemies;
using MTTPolish.GameStuff.Towers;
using System;
using System.Collections.Generic;

namespace MTTPolish.GameStuff.States
{
    /*
     * To-Do: Put trees at the top portion of the screen
     */
    internal class PlayState : IState
    {
        Texture2D pathTexture;
        Texture2D goblinTexture;

        Random randomNumberGenerator;
        Board level;
        List<Goblin> goblins;
        List<Frank> franks;

        public PlayState()
        {
            randomNumberGenerator = new Random();
            goblins = new List<Goblin>();
            franks = new List<Frank>();

            // For now, best to keep the dimensions the same aspect ratio as the window
            level = new Board(randomNumberGenerator, 18, 32);
        }

        public void Initialize()
        {
            level.Generate();
            level.Print();

            goblins.Add(new Goblin(level.Path));
            franks.Add(new Frank(level.Map[8, 7]));
        }

        public void LoadContent(ContentManager content)
        {
            pathTexture = content.Load<Texture2D>("Tiles/pathTexture");
            goblinTexture = content.Load<Texture2D>("Enemies/square");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < goblins.Count; i++)
                goblins[i].Move();

            for (int i = 0; i < franks.Count; i++)
                franks[i].Fire(goblins);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < level.Path.Length; i++) 
                level.Path[i].Draw(spriteBatch, pathTexture);

            for (int i = 0; i < goblins.Count; i++)
                goblins[i].Draw(spriteBatch, goblinTexture);

            for (int i = 0; i < franks.Count; i++)
                franks[i].Draw(spriteBatch, goblinTexture);
        }
    }
}
