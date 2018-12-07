# Evoventure
An adventure game based on [Evoland](http://www.evoland2.com/) that implements topics learned in an introductory graphics course.

## Premise
You are a renderer in a world that constantly changes appearance. Throughout your various quests and explorations, you will discover new graphical enhancements that will expand where and what you can do in the game. The world is extensive, but your rendering capabilities are not. In the beginning you will be limited to the number of graphical enhancements that can be active at a time. The combinations you choose will affect how you can interact with the environment.

___

# Developer Documentation
As assigned, the primary theme for this game is a journey. 

## Setting
Cyber-punk/Foresty [See Horizon Zero Dawn](https://www.playstation.com/en-us/games/horizon-zero-dawn-ps4/)

## Primary Features
(In order of priority)

* 2D Platforming
* Birds-eye 2D Exploration
* Title screen
* Save/load
* 3rd Person Control
* Combat
* Sound and Music
* Animations
* Different locomotion styles

## Secondary Features
* Shaders
* Agents
* Perlin noise
* Terrain generation
* Multiple input channels (keyboard, gamepad, VR, etc.)
* NPR to PBR transitions
* Flying
* Lighting
* Shadows
* Subdivision curves/surfaces

___
![Evoventure Pipeline](https://github.com/Grubbly/Evoventure/blob/master/evoventure.png)

#### Figure 1: In the beginning, up to three graphical enhancements are loaded into empty rendering slots (the middle boxes in the diagram). Depending on the equipped enhancements, the rendering could help or harm the player. Imagine a user has four graphical enhancements: 3rd person control, combat, sound and music, and animation. An example of a valid combination would be 3rd person control, sound and music, and animations, but the user would have to figure out ways to avoid combat because they would not be able to fight. Some enhancements are obviously incompatible like 2D platforming and 3rd person controls, so keep this in mind when implementing error checking.
