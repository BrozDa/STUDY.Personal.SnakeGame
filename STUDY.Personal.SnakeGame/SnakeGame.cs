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
        private readonly int _timerTick = 1000;
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

            gameBoard = new GameBoard(10,10,'#');

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

            //check if apple is on next position
            //if yes then insert new head to the start and generate new appl
            //if not then continue

            //NEW CODE
            /*SnakeBodyPart newHead = snake.CalculateNewHeadPosition(snake.currentDirection);
            if(CompareHeadAndApplePositions(apple, newHead))
            {
                snake.InsertNewHead(newHead);
                
            }
            CheckForApple(apple, newHead);*/

            //OLD CODE
            SnakeBodyPart newHead = snake.CalculateNewHeadPosition(snake.currentDirection);
            CheckForApple(apple, newHead);

            snake.UpdateSnake();

            //check if apple was eaten
            if(CompareHeadAndApplePositions(apple, snake.GetSnakeHead())){
                apple.GenerateNewApple(gameBoard, snake);
                DisplayManager.PrintApple(apple);
            }

            DisplayManager.PrintSnake(snake);
        }
        private bool CompareHeadAndApplePositions(Apple apple, SnakeBodyPart snakeHead)
        {
            return apple.xCoord == snakeHead.xCoord && apple.yCoord == snakeHead.yCoord;
        }
        private void CheckForApple(Apple apple, SnakeBodyPart newHead)
        {
            if(CompareHeadAndApplePositions(apple, newHead))
            {
              snake.InsertNewHead(newHead);
            }
        }


    }
}
