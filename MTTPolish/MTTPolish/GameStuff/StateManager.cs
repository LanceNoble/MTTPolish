using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MTTPolish.GameStuff
{
    /*enum GameStates
    {
        Menu,
        Play,
        Pause,
    }*/
    internal class StateManager
    {
        private static StateManager instance;
        private ContentManager content;
        private List<BaseState> possibleStates;
        private BaseState currentState;
        
        //private Stack<BaseState> states = new Stack<BaseState>();

        private StateManager()
        {
            //possibleStates[Convert.ToInt16(GameStates.Menu)] = 
            possibleStates = new List<BaseState>();
        }

        public static StateManager Instance 
        {
            get
            {
                if (instance == null)
                    instance = new StateManager();
                return instance;
            }
        }

        //public ContentManager Content { set { content = value; } }

        public List<BaseState> PossibleStates { get { return possibleStates; } }

        public BaseState CurrentState { get { return currentState; } }

        //public BaseState CurrentState { get; set; }

        /*public BaseState CurrentState 
        { 
            get { return currentState; }
            set 
            { 
                currentState = value;
                //currentState.LoadContent(content);
            } 
        }*/

        public void SetCurrentState(GameState state)
        {
            int stateInt = (int)state;
            currentState = possibleStates[stateInt];
        }

        /*public void AddState(BaseState state)
        {
            try
            {
                states.Push(state);
                states.Peek().Initialize();
                if (content != null)
                {
                    states.Peek().LoadContent(content);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveState()
        {
            if (states.Count > 0)
            {
                try
                {
                    BaseState state = states.Peek();
                    states.Pop();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void ClearStates()
        {
            while (states.Count > 0)
            {
                states.Pop();
            }
        }

        public void ChangeState(BaseState state)
        {
            try
            {
                ClearStates();
                AddState(state);
            }
            catch { }
        }

        public void Update(GameTime gameTime)
        {
            //currentState.Update(gameTime);
            try
            {
                if (states.Count > 0)
                {
                    states.Peek().Update(gameTime);
                }
            }
            catch(Exception ex) { }
        }

        public void Draw(SpriteBatch batch)
        {
            try
            {
                if (states.Count > 0)
                {
                    states.Peek().Draw(batch);
                }
            }
            catch { }
        }*/

        /*public void UnloadContent()
        {
            foreach (BaseState state in states)
            {
                state.UnloadContent();
            }
        }*/
    }
}
