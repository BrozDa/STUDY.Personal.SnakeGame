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

        public readonly char TopLeftCorner = '\u2554';
        public readonly char TopRightCorner = '\u2557';
        public readonly char BottomLeftCorner = '\u255A';
        public readonly char BottomCorner = '\u255D';

        public readonly char VerticalLine = '\u2551';
        public readonly char HorizontalLine = '\u2550';

        public readonly char VerticalLineWithLeft = '\u2563';
        public readonly char VerticalLineWithRight = '\u2560';

        public readonly char ArrowUp = '\u2191';
        public readonly char ArrowDown = '\u2193';
        public readonly char ArrowLeft = '\u2190';
        public readonly char ArrowRight = '\u2192';


        public GameBoard(int width, int height, char borderCharacter)
        {
            Width = width;
            Height = height;
            BorderCharacter = borderCharacter;
        }
    }
}
