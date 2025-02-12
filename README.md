# C# Console Snake Game

This project started as a simple Snake game written in a single class. Over the course of several weeks, it gradually evolved into its current state. While there are still many areas for improvement, I’ve decided to shift my focus to other projects for now.

The game window and core mechanics are inspired by Yordan Yordanov’s video: https://www.youtube.com/watch?v=NXLo5_5xDm8
The arrow-controlled menu is inspired by Michael Hadley’s video: https://www.youtube.com/watch?v=qAWhGEPMlS8

## Game Features

* Classic Snake Gameplay – The snake eats apples and grows longer.
* Main Menu with Customizable Settings – Players can adjust game settings before starting.
* Dynamic Menu Navigation – The menu responds to arrow key inputs in real time, highlighting the current selection.
* Settings Menu Interaction – The menu highlights the currently selected setting and updates based on user input.
* Wall Traversal Mode – Players can enable an option that allows the snake to pass through walls.
* Smooth Snake Movement – The snake’s head rotates based on its current direction.
* Ability to pause Game - Game can be paused anytime

## Challenges Faced

* Project Scope Expansion – What started as a small afternoon project quickly grew beyond my initial expectations.
* Working with Multiple Classes – This was my first time structuring a project across multiple classes. I had to learn how to properly connect and organize them into a single functional unit.
* Arrow Key Menu Navigation – After seeing a tutorial on this feature, I knew I wanted to implement it in one of my projects. It was an interesting challenge to get it working smoothly.
* Menu Navigation Logic – Ensuring that users returned to the correct previous menu, rather than always jumping back to the main menu, required careful structuring.
* Efficient Snake Rendering – Initially, the snake was represented as a 2D character array, with the entire grid being redrawn every game tick. This was inefficient and created screen flittering, so I later refactored it to use a List<SnakeBody> structure. Now, only the snake’s head is drawn at its new position while the tail is removed from the screen, improving performance.

## Lessons Learned

* Git & Version Control – This was the first project where I used Git. I started with the Visual Studio UI but later began learning Git via the command line.
* Scalability & Code Maintainability – Due to poor initial planning and limited coding experience, I often had to rewrite large portions of the code to accommodate new features. This taught me the importance of writing expandable and maintainable code from the start.
* Object-Oriented Programming (OOP) – As the project grew, the main class reached nearly 500 lines of code. Even though I could navigate it, it became unmanageable. Breaking the project into separate classes significantly improved readability and maintainability.

## Areas for Improvement

* Game Structure – The entire game runs from the main menu, which could be better structured.
* Progression System – The snake does not increase in speed over time, making gameplay less dynamic.
* General Project Design – I still have a lot to learn about software design principles and design patterns.

## Game Preview

https://github.com/user-attachments/assets/07211b8d-280a-49c3-bdf8-72a4d34e59a0

