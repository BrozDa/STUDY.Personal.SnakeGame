using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class GameBoard
    {
        public readonly int _width = 50;
        public readonly int _height = 20;
        private readonly char _borderCharacter = '#';

        public int[][] gameboard { get; set; }

        public GameBoard()
        {
        }
        public void PrintBorder()
        {
            StringBuilder board = new StringBuilder();
            board.Append(new string(_borderCharacter, _width) + '\n');
            for (int i = 1; i < _height - 1; i++)
            {
                board.Append(_borderCharacter);
                board.Append(new string(' ', _width - 2));
                board.Append(_borderCharacter);
                board.Append('\n');
            }
            board.Append(new string(_borderCharacter, _width) + '\n');

            Console.WriteLine(board.ToString());
        }
    }
}
