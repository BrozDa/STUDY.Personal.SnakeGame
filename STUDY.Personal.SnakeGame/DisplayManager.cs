using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    
    internal class DisplayManager
    {
        private GameBoard Board { get; init; }

        private readonly string pressEnterText = ">>> Press [ENTER] to start <<<";
        private int padding;
        public DisplayManager(GameBoard board)
        {
            this.Board = board;
            padding = (Board.Width - 2 - pressEnterText.Length) / 2;
        }

        public void PrintGameBorder()
        {
            
            StringBuilder controlsAndPauseLine = new StringBuilder();
            controlsAndPauseLine.Append(new string(' ', padding));
            controlsAndPauseLine.Append("Controls: " + Board.ArrowLeft + "   " + Board.ArrowRight);
            controlsAndPauseLine.Append("  Pause: [SPACE] ");
            controlsAndPauseLine.Append(new string(' ', Board.Width - controlsAndPauseLine.Length - 2));

            int upDownArrowPosition = controlsAndPauseLine.ToString().IndexOf(Board.ArrowLeft) + 2;


            StringBuilder upArrow = new StringBuilder();
            upArrow.Append(new string(' ', upDownArrowPosition) + Board.ArrowUp);
            upArrow.Append(new string(' ', Board.Width - upArrow.Length - 2));

            
            string downArrow = upArrow.ToString();
            downArrow = downArrow.Replace(Board.ArrowUp, Board.ArrowDown);

            string horizontalLineNoCorners = new string(Board.HorizontalLine, Board.Width - 2);


            Console.WriteLine(HorizontalLinesBetweenCorners(Board.TopLeftCorner, Board.TopRightCorner));


            for (int i = 1; i < Board.Height - 1; i++)
            {
                PrintEmptyLineWithBorders();
            }

            Console.WriteLine(HorizontalLinesBetweenCorners(Board.VerticalLineWithRight, Board.VerticalLineWithLeft));

            PrintEmptyLineWithBorders();

            Console.Write(Board.VerticalLine + new string(' ', padding));
            Console.Write(pressEnterText);
            Console.WriteLine(new string(' ', padding)+ Board.VerticalLine);

            PrintEmptyLineWithBorders();

            Console.WriteLine(WrapTextToBorders(upArrow.ToString()));

            Console.WriteLine(WrapTextToBorders(controlsAndPauseLine.ToString()));

            Console.WriteLine(WrapTextToBorders(downArrow));

            PrintEmptyLineWithBorders();

            Console.WriteLine(HorizontalLinesBetweenCorners(Board.BottomLeftCorner, Board.BottomCorner));


        }
        public string WrapTextToBorders(string text)
        {
            return Board.VerticalLine + text + Board.VerticalLine;
        }
        public string HorizontalLinesBetweenCorners(char leftSide, char rightSide)
        {
            return leftSide + new string(Board.HorizontalLine, Board.Width - 2) + rightSide;
        }
        public void PrintEmptyLineWithBorders()
        {
            Console.Write(Board.VerticalLine);
            Console.Write(new string(' ', Board.Width - 2));
            Console.Write(Board.VerticalLine);
            Console.WriteLine();
        }

        public void PrintApple(Apple apple) {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.SetCursorPosition(apple.xCoord, apple.yCoord);
            Console.Write(apple.appleCharacter);
            Console.ResetColor();   
        }

        public void PrintSnake(Snake snake) {

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
        public void RemoveTailFromScreen(SnakeBodyPart tail)
        {
     
            Console.SetCursorPosition(tail.xCoord, tail.yCoord);
            Console.Write(" ");
        }
    }
}
