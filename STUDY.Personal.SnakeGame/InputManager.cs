namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represent object managing all input for the snake game
    /// </summary>
    internal class InputManager
    {
        private Direction _direction;
        /// <summary>
        /// Initializes new object of Input Manager class
        /// </summary>
        /// <param name="defaultDirection"></param>
        public InputManager(Direction defaultDirection)
        {
            _direction = defaultDirection;
        }
        /// <summary>
        /// Process user input and return <see cref="Direction"/> enumeration value
        /// </summary>
        /// <param name="isPaused">Bolean value representing game status - true if pause, false otherwise</param>
        /// <returns><see cref="Direction"/> enumaration value representing direction of the snake based on user input</returns>
        public Direction ProcessUserInput(bool isPaused) 
        {
            ConsoleKey userInput = GetUserKey();

            if(userInput == ConsoleKey.Spacebar)
            {
                return isPaused? Direction.Resume: Direction.Pause;
            }
            else if (isPaused)
            {
                return Direction.Stand;
            }
            else
            {
                _direction = ConvertKeyToDirection(userInput);
                return _direction;
            }
        }
        /// <summary>
        /// Checks user input and returns respective <see cref="ConsoleKey"/> value
        /// </summary>
        /// <returns><see cref="ConsoleKey"/> value representing user input</returns>
        private ConsoleKey GetUserKey()
        {
            ConsoleKey? userInput = null;

            //clears whole buffer and takes only last input
            //if there is no input then returns ConsoleKey.None
            while (Console.KeyAvailable)
            {
                userInput = Console.ReadKey(true).Key;
            }

            return userInput ?? ConsoleKey.None;
        }/// <summary>
         /// Takes user input in form of <see cref="ConsoleKey"/> and return appropriate <see cref="Direction"/> value
         /// </summary>
         /// <param name="key"><see cref="ConsoleKey"/> character representing user input</param>
         /// <returns>Returs appropriate <see cref="Direction"/> value,<see cref="Direction"/>will not be changed in case of invalid input </returns>
        public Direction ConvertKeyToDirection(ConsoleKey key) 
        {
            return key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => _direction,
            };
        }
    }

    
}
