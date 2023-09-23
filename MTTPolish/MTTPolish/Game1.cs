using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MTTPolish.GameStuff;
using MTTPolish.GameStuff.States.DerivedStates;

namespace MTTPolish
{
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

            StateManager.Instance.PossibleStates.Add(new MenuState());
            StateManager.Instance.PossibleStates.Add(new PlayState());
            StateManager.Instance.PossibleStates.Add(new PauseState());
            StateManager.Instance.SetCurrentState(GameState.Menu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            for (int i = 0; i < StateManager.Instance.PossibleStates.Count; i++)
                StateManager.Instance.PossibleStates[i].LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*MouseState ms = Mouse.GetState();
            if (ms.LeftButton.Equals(ButtonState.Pressed)) 
                StateManager.Instance.SetCurrentState(GameState.Menu);

            else if (ms.RightButton.Equals(ButtonState.Pressed))
                StateManager.Instance.SetCurrentState(GameState.Play);

            else
                StateManager.Instance.SetCurrentState(GameState.Pause);*/


            // TODO: Add your update logic here
            StateManager.Instance.CurrentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            StateManager.Instance.CurrentState.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}