using System.Collections.Generic;
using MTTPolish.GameStuff.States;

namespace MTTPolish.GameStuff
{
    internal static class StateManager
    {
        private static List<IState> possibleStates;
        private static IState currentState;
        static StateManager()
        {
            possibleStates = new List<IState>();
        }

        public static List<IState> PossibleStates { get { return possibleStates; } }
        public static IState CurrentState { get { return currentState; } }

        public static void SetCurrentState(GameState state)
        {
            int stateInt = (int)state;
            currentState = possibleStates[stateInt];
        }
    }
}
