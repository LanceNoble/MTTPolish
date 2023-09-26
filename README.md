# MTTPolish
Polishing my team's very first project: "Magic: The Towering "

The first thing I noticed when I looked at the old code base was that the Game1.cs file was cluttered with game state management logic, so I refactored it via a game state management system.
This system involves a StateManager class that serves as a singleton responsible for keeping track of the currently active game state and switching states when necessary.
As for the exact data types this StateManager handles, that would be those who implement the IState interface: an interface that defines the behavior of all GameStates.
This behavior involves: initializing variables for game logic, updating game logic via those variables, loading the content that will be drawn for that game state, and drawing the loaded content based on what was updated in the game logic.

I remember someone asking me if the map of our game was randomly generated.
At the time, our game could only load levels via hard-typed text files, but now I'm here to implement some sort of procedural generation for our maps.
