using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class Apple
    {
        private int _xCoord;
        private int _yCoord;

        private GameBoard _board;
        private Snake _snake;
        public int XCoord { get { return _xCoord; } }
        public int YCoord { get { return _yCoord; } }



        public readonly char appleCharacter = '\u00F3'; //ó
        public Apple(GameBoard board, Snake snake)
        {
            _board = board;
            _snake = snake;
        }

        public void GenerateNewApple()
        {
            Random random = new Random();
            do
            {
                _xCoord = random.Next(1, _board.Width-1);
                _yCoord = random.Next(1, _board.Height-1);

            } while (!CheckIfValidPositionForApple());
        }

        public bool CheckIfValidPositionForApple() {
            List<SnakeBodyPart> snakeBody = _snake.GetSnakeBody();

            foreach (SnakeBodyPart sbpPart in snakeBody)
            {
                if (sbpPart.xCoord == _xCoord && sbpPart.yCoord == _yCoord)
                    return false;
            }
            return true;
        }
    }
}
