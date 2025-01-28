using System.Timers;

namespace STUDY.Personal.SnakeGame
{
    internal class SnakeGame
    {
        private readonly int _timerTick = 100;
        private readonly System.Timers.Timer _timer;
        private readonly GameBoard _gameBoard;
        private readonly Snake _snake;
        private readonly InputManager _inputManager;
        private readonly DisplayManager _displayManager;
        private readonly Apple _apple;
        private int _score = 0;
        private Direction direction = Direction.Right;
        private Direction beforePause;
        private bool _snakeAlive = true;
        private bool _isPaused = false;

        /// <summary>
        /// Initializes new instance of Snake Game class
        /// </summary>
        /// <param name="size"></param>
        /// <param name="canMoveThroughWalls"></param>
        public SnakeGame((int width, int height) size, bool canMoveThroughWalls)
        {
            _gameBoard = new GameBoard(size.width, size.height);
            _inputManager = new InputManager(direction);
            _displayManager = new DisplayManager(_gameBoard);
            _snake = new Snake(_gameBoard, direction, canMoveThroughWalls);
            _apple = new Apple(_gameBoard, _snake);
            _timer = new System.Timers.Timer();

        }
        /// <summary>
        /// Starts the game loop and manages it until the snake is dead
        /// </summary>
        public void PlayGame()
        {

            SetupNewGame();
            SetupTimer(_timerTick);
            _displayManager.PrintSnake(_snake);

            while (_snakeAlive)
            {
                //ongoing game loop, goes until the snake is alive
            }

            _displayManager.PrintDeadBanner(_score);
            Console.ReadKey(true);
            _timer.Stop();  

        }
        /// <summary>
        /// Setups componends to the new game of Snake
        /// </summary>
        private void SetupNewGame()
        {
            _displayManager.PrintGame();

            while (true)
            {
                if (Console.ReadKey(false).Key == ConsoleKey.Enter)
                    break;
            }
            _apple.GenerateNewApple();
            _displayManager.PrintApple(_apple);
            _displayManager.PrintScore(_score);
        }
        /// <summary>
        /// Initializes all necessary components of the timer
        /// </summary>
        /// <param name="timerTick"><see cref="System.Timers.Timer "/> interval, representing the game tick</param>
        private void SetupTimer(int timerTick)
        {
            _timer.Enabled = true;
            _timer.AutoReset = false;
            _timer.Interval = timerTick;
            _timer.Elapsed += GameTick;
        }
        /// <summary>
        /// <see cref="System.Timers.Timer"/> elapsed event handler representing game tick
        /// After every tick, the game checks user input, validates it and update output accordingly
        /// </summary>
        private void GameTick(object? sender, ElapsedEventArgs e)
        {
            Direction newDirection = _inputManager.ProcessUserInput(_isPaused);

            //Do nothing
            if (newDirection == Direction.Stand) { }

            //Game is paused and user pressed Space to resume the game
            else if (_isPaused && newDirection == Direction.Resume)
            {
                ResumeGame();
            }
            // User pressed Space to pause the game
            else if (!_isPaused && newDirection == Direction.Pause)
            {
                PauseGame();
            }
            // User is playing
            else
            {
                ProcessSnake(newDirection);
                Console.SetCursorPosition(0, 0);
            }
            _timer.Start();
        }
        /// <summary>
        /// Manages user request to pause the game
        /// </summary>
        private void PauseGame(){
            _isPaused = true;
            beforePause = direction;
            _displayManager.PrintPauseBanner();
        }
        /// <summary>
        /// Manages user request to resume the game
        /// </summary>
        private void ResumeGame() {
            _isPaused = false;
            direction = beforePause;
            _displayManager.ClearBanner();
            _displayManager.PrintApple(_apple);
            ProcessSnake(direction);
        }
        /// <summary>
        /// Takes <see cref="Direction"/> argument, and checks whether position of snake by following this direction is valid and update output accordingly
        /// Checks whether snake didnt hit a wall, ate himself or ate apple
        /// </summary>
        /// <param name="newDirection"><see cref="Direction"/> value representing new direction taken from user input</param>
        private void ProcessSnake(Direction newDirection)
        {
            direction = newDirection;
            //check user input for direction
            _snake.UpdateSnakeDirection(newDirection);
            _snake.UpdateSnakeHeadCharacter(direction);
            //calculate new position for head of the snake
            SnakeBodyPart newHead = _snake.GetNewHeadObject(_snake.CurrentDirection);

            //check if snake didnt crash to wall or ate himself
            bool validPositionForNewHead = _snake.ValidateNewHeadPosition(newHead);

            if (validPositionForNewHead == false)
            {
                _snakeAlive = false;
            }
            else
            {
                //check if the apple was eaten - eg position of head == position of apple
                bool appleEaten = CompareHeadAndApplePositions(_apple, newHead);

                //if apple was eaten then we need to generate and print new apple
                if (appleEaten)
                {
                    _apple.GenerateNewApple();
                    _displayManager.PrintApple(_apple);
                    _score++;
                    _displayManager.PrintScore(_score);
                }
                //update snake with position of head
                //if apple was eaten then we do not remove tail
                _displayManager.RemoveTailFromScreen(_snake.SnakeTail);
                _snake.UpdateSnake(newHead, appleEaten);
                _displayManager.PrintSnake(_snake);
            }
        }
        /// <summary>
        /// Check if apple and snake head are on same position - if snake ate apple
        /// </summary>
        /// <param name="apple"><see cref="Apple"/> object for comparison</param>
        /// <param name="snakeHead"><see cref="SnakeBodyPart"/> object representing snake head</param>
        /// <returns><see cref="bool"/>true, if apple was eaten</returns>
        private bool CompareHeadAndApplePositions(Apple apple, SnakeBodyPart snakeHead)
        {
            return apple.XCoord == snakeHead.XCoord && apple.YCoord == snakeHead.YCoord;
        }
        
        


    }
}
