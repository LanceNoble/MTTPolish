﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MTTPolish.Mechanics.StateStuff;
using MTTPolish.Mechanics.StateStuff.States;

namespace MTTPolish
{
    /*
     * Enums must map to increasing consecutive integers starting from 0
     * because StateManager uses GameState to set the current state
     * by using it as an index to iterate through its PossibleStates.
     */
    enum GameState
    {
        Menu,
        Play,
        Pause,
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            StateManager.PossibleStates.Add(new MenuState());
            StateManager.PossibleStates.Add(new PlayState());
            StateManager.PossibleStates.Add(new PauseState());

            StateManager.SetCurrentState(GameState.Play);

            for (int i = 0; i < StateManager.PossibleStates.Count; i++)
                StateManager.PossibleStates[i].Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            for (int i = 0; i < StateManager.PossibleStates.Count; i++)
                StateManager.PossibleStates[i].LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            StateManager.CurrentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            StateManager.CurrentState.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}