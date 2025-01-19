using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class GameBoard
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public char BorderCharacter { get; set; }


        public GameBoard(int width, int height, char borderCharacter)
        {
            Width = width;
            Height = height;
            BorderCharacter = borderCharacter;
        }
    }
}
