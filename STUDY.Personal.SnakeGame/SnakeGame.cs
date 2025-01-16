using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class SnakeGame
    {
        private readonly int _timerTick = 1000;
        private System.Timers.Timer _timer;
        private readonly char _snakeChar = '@';
        private GameBoard gameBoard { get; set; }
        private Snake snake { get; set; }

        public SnakeGame()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            gameBoard = new GameBoard();
            gameBoard.PrintBorder();

            snake = new Snake(gameBoard);
            snake.PrintSnake(_snakeChar);
            SetupTimer(_timerTick);
        }
        public void SetupTimer(int timerTick)
        {
            _timer = new System.Timers.Timer();
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Interval = timerTick;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(delegate { snake.PrintSnake(_snakeChar); });
            
        }
        
    }
}
