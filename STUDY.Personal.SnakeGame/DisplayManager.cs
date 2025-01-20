using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    
    internal static class DisplayManager
    {

        

        public static void PrintGameBorder(GameBoard board)
        {
            string pressEnterText = ">>> Press [ENTER] to start <<<";
            int padding = (board.Width-2 - pressEnterText.Length) / 2;


            StringBuilder controlsAndPauseLine = new StringBuilder();
            controlsAndPauseLine.Append(new string(' ', padding));
            controlsAndPauseLine.Append("Controls: " + board.ArrowLeft + "   " + board.ArrowRight);
            controlsAndPauseLine.Append("  Pause: [SPACE] ");
            controlsAndPauseLine.Append(new string(' ', board.Width - controlsAndPauseLine.Length - 2));

            int upDownArrowPosition = controlsAndPauseLine.ToString().IndexOf(board.ArrowLeft) + 2;


            StringBuilder upArrow = new StringBuilder();
            upArrow.Append(new string(' ', upDownArrowPosition) + board.ArrowUp);
            upArrow.Append(new string(' ', board.Width - upArrow.Length - 2));

            
            string downArrow = upArrow.ToString();
            downArrow = downArrow.Replace(board.ArrowUp, board.ArrowDown);

            string horizontalLineNoCorners = new string(board.HorizontalLine, board.Width - 2);


            Console.WriteLine(HorizontalLinesBetweenCorners(board, board.TopLeftCorner, board.TopRightCorner));


            for (int i = 1; i < board.Height - 1; i++)
            {
                PrintEmptyLineWithBorders(board);
            }

            Console.WriteLine(HorizontalLinesBetweenCorners(board, board.VerticalLineWithRight, board.VerticalLineWithLeft));

            PrintEmptyLineWithBorders(board);

            Console.Write(board.VerticalLine + new string(' ', padding));
            Console.Write(pressEnterText);
            Console.WriteLine(new string(' ', padding)+ board.VerticalLine);

            PrintEmptyLineWithBorders(board);

            Console.WriteLine(WrapTextToBorders(board, upArrow.ToString()));

            Console.WriteLine(WrapTextToBorders(board, controlsAndPauseLine.ToString()));

            Console.WriteLine(WrapTextToBorders(board, downArrow));

            PrintEmptyLineWithBorders(board);

            Console.WriteLine(HorizontalLinesBetweenCorners(board, board.BottomLeftCorner, board.BottomCorner));


        }
        public static string WrapTextToBorders(GameBoard board, string text)
        {
            return board.VerticalLine + text + board.VerticalLine;
        }
        public static string HorizontalLinesBetweenCorners(GameBoard board, char leftSide, char rightSide)
        {
            return leftSide + new string(board.HorizontalLine, board.Width - 2) + rightSide;
        }
        public static void PrintEmptyLineWithBorders(GameBoard board)
        {
            Console.Write(board.VerticalLine);
            Console.Write(new string(' ', board.Width - 2));
            Console.Write(board.VerticalLine);
            Console.WriteLine();
        }

        public static void PrintApple(Apple apple) {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.SetCursorPosition(apple.xCoord, apple.yCoord);
            Console.Write(apple.appleCharacter);
            Console.ResetColor();   
        }

        public static void PrintSnake(Snake snake) {

            Console.ForegroundColor = ConsoleColor.Green;
            List<SnakeBodyPart> snakeBody = snake.GetSnakeBody();

           // RemoveTailFromScreen(snakeBody[snakeBody.Count-1]);

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
            Console.ResetColor();
        }
        public static void RemoveTailFromScreen(SnakeBodyPart tail)
        {
     
            Console.SetCursorPosition(tail.xCoord, tail.yCoord);
            Console.Write(" ");
        }
    }
}
