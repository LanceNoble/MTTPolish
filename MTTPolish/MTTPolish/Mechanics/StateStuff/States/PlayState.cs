﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MTTPolish.Mechanics.Goblins;
using MTTPolish.Mechanics.Mages;
using MTTPolish.Mechanics.StateStuff;
using System;
using System.Collections.Generic;

namespace MTTPolish.Mechanics.StateStuff.States
{
    /*
     * To-Do: Put trees at the top portion of the screen
     */
    internal class PlayState : IState
    {
        // Player
        private int hp;
        private int gold;

        private Texture2D lPath;
        private Texture2D tPath;
        private Texture2D straightPath;
        private Texture2D grass;
        private Texture2D goblinTexture;

        private Random randomNumberGenerator;
        private Board level;
        private List<Goblin> goblins;
        private List<Mage> franks;

        public PlayState()
        {
            hp = 3;
            gold = 0;

            randomNumberGenerator = new Random();
            goblins = new List<Goblin>();
            franks = new List<Mage>();
            //level = new Board(randomNumberGenerator, 200, 200);
            level = new Board(randomNumberGenerator, 16, 9);
            //level = new Board(randomNumberGenerator, 32, 18);
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
            goblinTexture = content.Load<Texture2D>("temp/square");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < goblins.Count; i++)
                goblins[i].Move();

            for (int i = 0; i < franks.Count; i++)
                franks[i].Seek(goblins);
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
