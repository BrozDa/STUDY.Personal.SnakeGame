using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace STUDY.Personal.SnakeGame
{
    internal class SnakeGame
    {
        private readonly int _timerTick = 200;
        private System.Timers.Timer _timer;
        private readonly char _snakeChar = '@';
        private GameBoard gameBoard { get; set; }
        private Snake snake { get; set; }

        private Apple apple;

        private Direction defaultDirection = Direction.Right;

        private InputManager inputManager { get; set; }
        private bool snakeAlive = true;
        public SnakeGame()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            gameBoard = new GameBoard(50,20,'#');

            inputManager = new InputManager(defaultDirection);
            
            snake = new Snake(gameBoard, inputManager, defaultDirection);

            apple = new Apple();
            
            _timer = new System.Timers.Timer();
            
        }
        
        public void SetupNewGame()
        {
            SetupTimer(_timerTick);
            DisplayManager.PrintGameBorder(gameBoard);

            apple.GenerateNewApple(gameBoard, snake);
            DisplayManager.PrintApple(apple);


        }
        public void PlayGame()
        {
            SetupNewGame();
            DisplayManager.PrintSnake(snake);

            while (snakeAlive)
            {
            }

        }
        public void SetupTimer(int timerTick)
        {
            
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Interval = timerTick;
            _timer.Elapsed += GameTick;
            

        }

        private void GameTick(object? sender, ElapsedEventArgs e)
        {
            snake.UpdateSnakeDirection();
            snake.UpdateSnake();
            DisplayManager.PrintSnake(snake);
        }


    }
}
