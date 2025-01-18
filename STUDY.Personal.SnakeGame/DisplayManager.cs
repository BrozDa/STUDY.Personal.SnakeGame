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

        public static void PrintSnake(Snake snake) {
            List<SnakeBodyPart> snakeBody = snake.GetSnakeBody();

            RemoveTailFromScreen(snakeBody[snakeBody.Count-1]);

            char headCharacter = snake.GetSnakeHeadCharacter();
            char bodyCharacter = snake.GetSnakeBodyCharacter();


            

            Console.SetCursorPosition(snakeBody[0].xCoord, snakeBody[0].yCoord);
            Console.Write(headCharacter);

            for (int i = 1; i < snakeBody.Count; i++)
            {
                int x = snakeBody[i].xCoord;
                int y = snakeBody[i].yCoord;
                Console.SetCursorPosition(x, y);
                Console.Write(bodyCharacter);
            }
        }
        public static void RemoveTailFromScreen(SnakeBodyPart tail)
        {
     
            Console.SetCursorPosition(tail.xCoord, tail.yCoord);
            Console.Write("");
        }
    }
}
