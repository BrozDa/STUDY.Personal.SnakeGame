using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal static class DisplayManager
    {
        public static void PrintGameBorder(GameBoard board)
        {
            Console.WriteLine(new string(board.BorderCharacter, board.Width));
            for (int i = 1; i < board.Height-1; i++) {
                Console.Write(board.BorderCharacter);
                Console.Write(new string(' ', board.Width-2));
                Console.Write(board.BorderCharacter);
                Console.WriteLine();
            }
            Console.WriteLine(new string(board.BorderCharacter, board.Width));

        }

        public static void PrintApple(Apple apple) {
            Console.SetCursorPosition(apple.xCoord, apple.yCoord);
            Console.Write(apple.appleCharacter);
        }
    }
}
