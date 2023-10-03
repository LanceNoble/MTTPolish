# Game States
Game1.cs is a built-in file that comes with projects using the MonoGame framework. Whatever's in it will be run when the program starts. This file is incomprehensible in the old version mainly due to the shoddy game state management logic we stuffed into it. It all stemmed from a GameState enum and what resulted was a bunch of switch statements telling the game what to do based on its current value. On top of that, all of the game's data were being handled in this one file, so you couldn't even tell if this texture we loaded was meant to be drawn in the main menu or the pause menu.

I cleaned up Game1.cs by implementing a [game state management system](https://www.reddit.com/r/monogame/comments/672hc9/game_state_management_tutorial/). The first part to this is a StateManager singleton whose sole responsibility is to change the state of the game. It does this by first acquiring information about all the game states that implement IState. With this system, you can now tell which logic and data is meant for which game state.


# Levels
We planned for our game to be a 2D tower defense similar to Bloons TD. Enemies have a set path they must follow, and they must all be eliminated before they reach the end of it. This path was generated by reading from a hard-typed text file. While this did work, it did not add much variability to the game. Enemies would follow the same exact path every playthrough.

What I wanted to do was implement an algorithm that would procedurally generate the path. The first thought that came to mind was to use recursive backtracking.





Since our game was 2D, we thought it was best to make our levels tile-based, and what better way to do that than to load them from files. This was done in Board.cs, and while it did work fine, it didn't add much variability to the game. Enemies would follow the same path every playthrough

The next thing I wanted to do was add a feature for procedural enemy path generation to add more variability and replayability to the game,
so I implemented some sort of backtracking via a stack to force the path to follow any input constraints or rules for generation.

The third thing I wanted to do was shift responsibility between classes.
The Board class in the old file seemed to be responsible for doing basically everything: from moving enemies to handling collisions between them and towers.
This resulted in the Board class being cluttered with 500 lines of code.
I managed to reduce it to 100 by delegating enemy movement to enemy classes and delegating enemy elimination to tower classes
