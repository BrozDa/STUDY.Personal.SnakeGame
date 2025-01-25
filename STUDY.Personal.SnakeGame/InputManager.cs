using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace STUDY.Personal.SnakeGame
{
    internal class InputManager
    {
        Direction currentDirection { get; set; }
        public InputManager(Direction defaultDirection)
        {
            currentDirection = defaultDirection;
        }

        public Direction ProcessUserInput(bool isPaused) {

            ConsoleKey userInput = GetUserKey();

            if(userInput == ConsoleKey.Spacebar)
            {
                return isPaused? Direction.Resume: Direction.Pause;
            }


            if (isPaused)
            {
                return Direction.Stand;
            }

            currentDirection = ConvertKeyToDirection(userInput);
            return currentDirection;
            

        }

        public ConsoleKey GetUserKey()
        {
            ConsoleKey userInput = ConsoleKey.None;

            //clears whole buffer and takes only last input
            //if there is no input then returns ConsoleKey.None
            while (Console.KeyAvailable)
            {
                userInput = Console.ReadKey(true).Key;
            }
            return userInput;

        }
        public Direction ConvertKeyToDirection(ConsoleKey key) {


            return key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                ConsoleKey.None => currentDirection,
                _ => throw new NotImplementedException("Invalid ConsoleKey passed to ConvertKeyToDirection() method")

            };
        }
    }

    
}
