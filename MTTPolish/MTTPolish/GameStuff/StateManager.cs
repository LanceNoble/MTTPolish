using System.Collections.Generic;
using MTTPolish.GameStuff.States;

namespace MTTPolish.GameStuff
{
    internal class StateManager
    {
        private static StateManager instance;
        private List<IState> possibleStates;
        private IState currentState;
        
        private StateManager()
        {
            possibleStates = new List<IState>();
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
        public List<IState> PossibleStates { get { return possibleStates; } }
        public IState CurrentState { get { return currentState; } }

        public void SetCurrentState(GameState state)
        {
            int stateInt = (int)state;
            currentState = possibleStates[stateInt];
        }
    }
}
