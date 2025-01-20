using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

            gameBoard = new GameBoard(100,20,'#');

            inputManager = new InputManager(defaultDirection);
            
            snake = new Snake(gameBoard, inputManager, defaultDirection);

            apple = new Apple();
            
            _timer = new System.Timers.Timer();
            
        }
        
        public void SetupNewGame()
        {
            
            DisplayManager.PrintGameBorder(gameBoard);

            apple.GenerateNewApple(gameBoard, snake);
            DisplayManager.PrintApple(apple);


        }
        public void PlayGame()
        {
            SetupNewGame();
            SetupTimer(_timerTick);
            DisplayManager.PrintSnake(snake);

            while (snakeAlive)
            {
                
            }

            Console.SetCursorPosition(0, 0);    
            Console.WriteLine("SNAKE IS DEAD");

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
            //check user input for direction
            snake.UpdateSnakeDirection();

            //calculate new position for head of the snake
            SnakeBodyPart newHead = snake.CalculateNewHeadPosition(snake.currentDirection);

            //check if snake didnt crash to wall or ate himself
            bool validPositionForNewHead = snake.ValidateNewHeadPosition(newHead);

            if (validPositionForNewHead == false) { 
                snakeAlive = false;
            }
            else
            {
                //check if the apple was eaten - eg position of head == position of apple
                bool appleEaten = CompareHeadAndApplePositions(apple, newHead);

                //if apple was eaten then we need to generate and print new apple
                if (appleEaten)
                {
                    apple.GenerateNewApple(gameBoard, snake);
                    DisplayManager.PrintApple(apple);
                }
                //update snake with position of head
                //if apple was eaten then we do not remove tail
                snake.UpdateSnake(newHead, appleEaten);
                DisplayManager.PrintSnake(snake);
            }
            

            

            //print the snake
            
        }
        private bool CompareHeadAndApplePositions(Apple apple, SnakeBodyPart snakeHead)
        {
            return apple.xCoord == snakeHead.xCoord && apple.yCoord == snakeHead.yCoord;
        }
        


    }
}
