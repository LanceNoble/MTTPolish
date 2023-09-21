using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MTTPolish.GameStuff;
using MTTPolish.GameStuff.States;
using System;

namespace MTTPolish
{
    // Game States:
    // Menu
    // Game
    // Pause
    // Over
    // Next
    enum GameState
    {
        Menu,
        Play,
        Pause,
    }
    public class Game1 : Game
    {
        /*private enum GameState
        {
            Menu,
            Play,
            Pause,
        }*/
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

            StateManager.Instance.PossibleStates.Add(new MenuState(_graphics));
            StateManager.Instance.PossibleStates.Add(new PlayState(_graphics));
            StateManager.Instance.PossibleStates.Add(new PauseState(_graphics));
            StateManager.Instance.SetCurrentState(GameState.Menu);
            //StateManager.Instance.CurrentState = StateManager.Instance.PossibleStates[(int)GameState.Menu];
            //StateManager.Instance.PossibleStates[(int)GameState.Menu] = new MenuState(GraphicsDevice);
            //StateManager.Instance.PossibleStates[(int)GameState.Play] = new PlayState(GraphicsDevice);
            //StateManager.Instance.PossibleStates[(int)GameState.Pause] = new MenuState(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //StateManager.Instance.Content = Content;
            for (int i = 0; i < StateManager.Instance.PossibleStates.Count; i++)
                StateManager.Instance.PossibleStates[i].LoadContent(Content);
            
            // TODO: use this.Content to load your game content here
           
            //StateManager.Instance.PossibleStates[Convert.ToInt16(GameState.Menu)] = new MenuState(GraphicsDevice);
            //StateManager.Instance.AddState(new MenuState(GraphicsDevice));
        }

        /*protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            StateManager.Instance.UnloadContent();
        }*/

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //StateManager.Instance.Update(gameTime);
            StateManager.Instance.CurrentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            //StateManager.Instance.Draw(_spriteBatch);
            StateManager.Instance.CurrentState.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}