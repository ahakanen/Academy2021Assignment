# Academy2021Assignment

- Whenever left mouse button is clicked, the player's velocity will be set to an upwards "jump" velocity, and on every frame gravity is added to player's velocity, accelerating the player downwards. Just below the screen's edge is a special obstacle "KillBox" that kills the player on touch.
- Whenever the player moves above halfway of the screen, player and the "object controller" are nudged down so that the player stays in the middle, while everything else will move down with object controller.
- Objects and obstacles are both recycled, they are "spawned" by moving them to the middle where they will fall towards the player, and "despawned" by moving them far off the side of the screen.
- Obstacles that also include stars, will be rebuilt every time they are despawned so they can respawn consistently, as picking up the stars will despawn the stars individually and break the obstacle.
- Obstacles are created from different colors, shuffled randomly every time they are spawned.
- Obstacles will only register collision (except for the BottomKillBox) if the color of the player is not the same as the color of the colliding obstacle piece.
- On collision with an obstacle, player dies.
- If player is dead, left mouse clicks will restart the game instead of jumping.
- Objects (stars and color switchers) both give the player score.
- I chose to add a score multiplier mechanic to reward taking risks and progressing through obstacles fast.
- The score multiplier resets after not gaining any score for 10 seconds.
- I skipped making a main menu, as there's currently no features that would require it, and not having one is actually beneficial for testing the basic gameplay.
- I chose to leave some of the debug text in to make it easier to see what's going on when testing the gameplay.
- The naming of obstacle pieces is based on the color it appears as in editor, but in game the colors won't match as they are randomized. I chose to leave the naming and coloring as it is, because it helps manage the obstacles in editor/hierarchy/inspector better.
- Due to the assignment being a prototype, not a finished game, I didn't pay as much effort into finding alternate ways to code something better. "Once stuff works the way it should, as long as it's possible to read the code it's probably good enough for a prototype," is the guideline I followed, optimizing performance and such was secondary.

Features I'd like to add:
- Main menu
- Difficulty options
- More obstacles
- Ability to make pre-made levels as well instead of only an infinite random level

Known issues/bugs:

1. Obstacles that use stars as a part of themselves, will keep rotating the despawned stars. They should rotate outside the gameview, so shouldn't be a big deal.
