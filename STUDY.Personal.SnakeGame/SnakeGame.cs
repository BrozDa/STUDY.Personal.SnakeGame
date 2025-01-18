using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class SnakeGame
    {
        private readonly int _timerTick = 200;
        private System.Timers.Timer _timer;
        private readonly char _snakeChar = '@';
        private GameBoard gameBoard { get; set; }
        private Snake snake { get; set; }
        private Direction defaultDirection = Direction.Right;

        private InputManager inputManager { get; set; }
        private bool snakeAlive = true;
        public SnakeGame()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            gameBoard = new GameBoard();
            gameBoard.PrintBorder();

            inputManager = new InputManager(defaultDirection);

            snake = new Snake(gameBoard, inputManager, defaultDirection);
            snake.PrintSnake(_snakeChar);

            

            _timer = new System.Timers.Timer();
            SetupTimer(_timerTick);
        }
        public void PlayGame()
        {
            while (snakeAlive) { 
                
            }
        }
        public void SetupTimer(int timerTick)
        {
            
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Interval = timerTick;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(delegate { snake.PrintSnake(_snakeChar);});

            
        }
        
    }
}
