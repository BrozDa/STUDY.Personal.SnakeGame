using System.Drawing;
using System.Timers;

namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represent an Snake Game object managing the game
    /// </summary>
    internal class SnakeGame
    {
        private readonly int _timerTick = 100;
        private readonly InputManager _inputManager;
        private readonly MainMenu _menu;
        private GameBoard? _gameBoard;
        private DisplayManager? _displayManager;
        private Snake? _snake;
        private Apple? _apple;
        private System.Timers.Timer _timer;
        private int _score = 0;
        private Direction _direction = Direction.Right;
        private Direction _directionBeforePause;
        private bool _snakeAlive = true;
        private bool _isPaused = false;

        public SnakeGame()
        {;
            _inputManager = new InputManager(_direction);
            _timer = new System.Timers.Timer();
            _menu = new MainMenu(this);
        }
        /// <summary>
        /// Initializes new object of Snake Game class
        /// </summary>
        /// <param name="size">(<see cref="int"/>,<see cref="int"/> tuple representing size of the <see cref="GameBoard"/></param>
        /// <param name="canMoveThroughWalls"><see cref="bool"/> value indicating if snake is able to move through walls</param>
        /*public SnakeGame((int width, int height) size, bool canMoveThroughWalls)
        {
            _gameBoard = new GameBoard(size.width, size.height);
            _inputManager = new InputManager(_direction);
            _displayManager = new DisplayManager(_gameBoard);
            _snake = new Snake(_gameBoard, _direction, canMoveThroughWalls);
            _apple = new Apple(_gameBoard, _snake);       
            _timer = new System.Timers.Timer();
            _menu = new MainMenu(this);
        }*/
        /// <summary>
        /// Prints game field, awaits user input to start the game and generates all necessities to start the game
        /// </summary>
        public void UpdateSnakeGameSettings(int boardHeight, int boardWidth, bool canMoveThroughWalls)
        {
            _gameBoard = new GameBoard(boardHeight, boardWidth);
            _displayManager = new DisplayManager(_gameBoard);
            _snake = new Snake(_gameBoard, _direction, canMoveThroughWalls);
            _apple = new Apple(_gameBoard, _snake);
            
        }
        private void SetupNewRound()
        {
            if(_displayManager == null || _apple == null || _snake == null)
            {
                throw new NullReferenceException("Null object refered in SnakeGame.SetupNewRound()");
            }

            _displayManager.PrintGame();

            while (true)
            {
                if (Console.ReadKey(false).Key == ConsoleKey.Enter)
                    break;
            }

            _apple.GenerateNewApple();
            _displayManager.PrintApple(_apple);
            _displayManager.PrintScore(_score);
            _displayManager.PrintSnake(_snake);

              
        }
        /// <summary>
        /// Starts the game of Snake
        /// </summary>
        public void StartGame()
        {
            _menu.StartMenu();
        }
        /// <summary>
        /// Represents game loop for the Snake game
        /// </summary>
        public void PlayGame()
        {
            if (_displayManager == null || _timer == null)
            {
                throw new NullReferenceException("Null object refered in SnakeGame.PlayGame()");
            }

            SetupNewRound();
            SetupTimer(_timerTick);

            while (_snakeAlive){ 
                //infinite loop for the game which goes as long as snake is alive
            }
            _timer.Stop();
            _displayManager.PrintDeadBanner(_score);
            _inputManager.ProcessEndGame();

        }
        
        /// <summary>
        /// Setups timer used for the whole game, timer elapsed event updates the game based on user input
        /// </summary>
        /// <param name="timerTick"><see cref="int"/> value representing game tick in ms</param>
        private void SetupTimer(int timerTick)
        { 
            _timer.Enabled = true;
            _timer.AutoReset = false;
            _timer.Interval = timerTick;
            _timer.Elapsed += GameTick;  
        }
        /// <summary>
        /// Represents game tick using elapsed timer event. Process user input and updates snake accordingly
        /// Timer is reset at the end of processing
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
        /// Handles pausing of  the game
        /// </summary>
        private void PauseGame()
        {
            if (_displayManager == null)
            {
                throw new NullReferenceException("Null object refered in SnakeGame.PauseGame()");
            }

            _isPaused = true;
            _directionBeforePause = _direction;
            _displayManager.PrintPauseBanner();
        }
        /// <summary>
        /// Handles resuming of the game
        /// </summary>
        private void ResumeGame() 
        {
            if (_displayManager == null || _apple == null)
            {
                throw new NullReferenceException("Null object refered in SnakeGame.ResumeGame()");
            }

            _isPaused = false;
            _direction = _directionBeforePause;
            _displayManager.ClearBanner();
            _displayManager.PrintApple(_apple);
            ProcessSnake(_direction);
        }
        
        /// <summary>
        /// Move snake, checks if the position is valid, eg. Snake is not dead, checks if apple was eaten and update all necessary details accordingly
        /// </summary>
        /// <param name="newDirection"><see cref="Direction"/>value representing the direction of a snake</param>
        private void ProcessSnake(Direction newDirection)
        {
            if(_snake == null || _apple == null || _displayManager == null)
            {
                throw new NullReferenceException("Null object refered in SnakeGame.ProcessSnake()");
            }

            _direction = newDirection;
            //check user input for direction
            _snake.UpdateSnakeDirection(newDirection);

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
        /// Compares possition of an apple and head of the snake
        /// </summary>
        /// <param name="apple"><see cref="Apple"/> object representing current apple on screen</param>
        /// <param name="snakeHead"><see cref="SnakeBodyPart"/> object representing head of the snake</param>
        /// <returns><see cref="bool"/> true if head and apple are on the same position, false otherwise</returns>
        private bool CompareHeadAndApplePositions(Apple apple, SnakeBodyPart snakeHead)
        {
            return apple.XCoord == snakeHead.XCoord && apple.YCoord == snakeHead.YCoord;
        }
    }
}
