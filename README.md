# MTTPolish
Polishing my team's very first project: "Magic: The Towering "

The first thing I noticed when I looked at the old code base was that the Game1.cs file was cluttered with game state management logic, so I refactored it via a game state management system.
This system involves a StateManager class that serves as a singleton responsible for keeping track of the currently active game state and switching states when necessary.
As for the exact data types this StateManager handles, that would be those who implement the IState interface: an interface that defines the behavior of all GameStates.
This behavior involves: initializing variables for game logic, updating game logic via those variables, loading the content that will be drawn for that game state, and drawing the loaded content based on what was updated in the game logic.

The next thing I wanted to do was add a feature for procedural enemy path generation to add more variability and replayability to the game,
so I implemented some sort of backtracking via a stack to force the path to follow any input constraints or rules for generation.

The third thing I wanted to do was shift responsibility between classes.
The Board class in the old file seemed to be responsible for doing basically everything: from moving enemies to handling collisions between them and towers.
This resulted in the Board class being cluttered with 500 lines of code.
I managed to reduce it to 100 by delegating enemy movement to enemy classes and delegating enemy elimination to tower classes
