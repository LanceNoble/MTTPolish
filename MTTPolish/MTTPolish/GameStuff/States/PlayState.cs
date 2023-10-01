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
        Texture2D lPath;
        Texture2D tPath;
        Texture2D straightPath;
        Texture2D grass;

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
            level = new Board(randomNumberGenerator, 32, 18);
        }

        public void Initialize()
        {
            //level = new Board(randomNumberGenerator, 32, 18, grass, lPath, tPath, straightPath);
            level.Generate();
            level.Print();

            //goblins.Add(new Goblin(level.Path));
            //franks.Add(new Frank(level.Map[17, 15]));
        }

        public void LoadContent(ContentManager content)
        {
            grass = content.Load<Texture2D>("Tiles/grass");
            lPath = content.Load<Texture2D>("Tiles/lPath");
            tPath = content.Load<Texture2D>("Tiles/tPath");
            straightPath = content.Load<Texture2D>("Tiles/straightPath");
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
            level.Draw(spriteBatch, grass, lPath, tPath, straightPath);

            for (int i = 0; i < goblins.Count; i++)
                goblins[i].Draw(spriteBatch, goblinTexture);

            for (int i = 0; i < franks.Count; i++)
                franks[i].Draw(spriteBatch, goblinTexture);
        }
    }
}
