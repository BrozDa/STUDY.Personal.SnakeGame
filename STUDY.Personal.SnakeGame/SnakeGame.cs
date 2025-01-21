﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
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

        private Direction direction = Direction.Right;
        private Direction beforePause;

        private InputManager inputManager { get; set; }
        private DisplayManager displayManager { get; set; }

        private bool snakeAlive = true;
        private bool _isPaused = false;
        public SnakeGame()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            gameBoard = new GameBoard(50,20,'#');

            inputManager = new InputManager(direction);

            displayManager = new DisplayManager(gameBoard);

            snake = new Snake(gameBoard, inputManager, direction);

            apple = new Apple();
            
            _timer = new System.Timers.Timer();
            
        }
        
        public void SetupNewGame()
        {

            displayManager.PrintGameBorder();

            apple.GenerateNewApple(gameBoard, snake);
            displayManager.PrintApple(apple);


        }
        public void PlayGame()
        {
            SetupNewGame();
            /*while(Console.ReadKey().Key != ConsoleKey.Enter)
            {

            }*/
            SetupTimer(_timerTick);
            displayManager.PrintSnake(snake);

            while (snakeAlive)
            {
                
            }

            Console.SetCursorPosition(0, 0);    
            Console.WriteLine("SNAKE IS DEAD");

        }
        public void SetupTimer(int timerTick)
        {
            
            _timer.Enabled = true;
            _timer.AutoReset = false;
            _timer.Interval = timerTick;
            _timer.Elapsed += GameTick;
            
           
        }

        private void GameTick(object? sender, ElapsedEventArgs e)
        {
            Direction newDirection = inputManager.ProcessUserInput(_isPaused);

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
                PlayRound(newDirection);
                //direction = newDirection;
                //ProcessSnake(newDirection);

            }

            _timer.Start();


        }
        private void PauseGame(){
            _isPaused = true;
            beforePause = direction;
        }
        private void ResumeGame() {
            _isPaused = false;
            direction = beforePause;
            ProcessSnake(direction);
        }
        private void PlayRound(Direction newDirection) {
            direction = newDirection;
            ProcessSnake(newDirection);
        }

        private void ProcessSnake(Direction newDirection)
        {
            //check user input for direction
            snake.UpdateSnakeDirection(newDirection);

            //calculate new position for head of the snake
            SnakeBodyPart newHead = snake.CalculateNewHeadPosition(snake.currentDirection);

            //check if snake didnt crash to wall or ate himself
            bool validPositionForNewHead = snake.ValidateNewHeadPosition(newHead);

            if (validPositionForNewHead == false)
            {
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
                    displayManager.PrintApple(apple);
                }
                //update snake with position of head
                //if apple was eaten then we do not remove tail
                displayManager.RemoveTailFromScreen(snake.GetSnakeTail());
                snake.UpdateSnake(newHead, appleEaten);
                displayManager.PrintSnake(snake);
            }
        }
        private bool CompareHeadAndApplePositions(Apple apple, SnakeBodyPart snakeHead)
        {
            return apple.xCoord == snakeHead.xCoord && apple.yCoord == snakeHead.yCoord;
        }
        
        


    }
}
