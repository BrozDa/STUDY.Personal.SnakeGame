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
        private readonly int scoreRow;
        private readonly int scoreCol;
        private int padding;
        string controlsAndPauseLine;
        int upDownArrowPosition;

        

        public DisplayManager(GameBoard board)
        {
            this.Board = board;
            padding = (Board.Width - 2 - pressEnterText.Length) / 2;
            controlsAndPauseLine = ControlsAndPauseLine();
            upDownArrowPosition = controlsAndPauseLine.ToString().IndexOf(Board.ArrowLeft) + 2;
            scoreRow = board.Height + 1;
            scoreCol = 1 + padding + 12;
        }

        public void PrintGameField()
        {
            string topBorder = HorizontalLinesBetweenCorners(Board.TopLeftCorner, Board.TopRightCorner);
            string middleBorder = HorizontalLinesBetweenCorners(Board.VerticalLineWithRight, Board.VerticalLineWithLeft);
            Console.WriteLine(topBorder);
            PrintEmptyLineWithBorders(Board.Height - 2);
            Console.WriteLine(middleBorder);
        }
        public void PrintGameBorder()
        {
            
            string bottomBorder = HorizontalLinesBetweenCorners(Board.BottomLeftCorner, Board.BottomCorner);

            string controlsAndPauseLine = ControlsAndPauseLine();

            PrintGameField();
            
            PrintEmptyLineWithBorders(1);
            PrintCenteredText(pressEnterText);
            PrintEmptyLineWithBorders(1);
            PrintArrowsAndControls();
            PrintEmptyLineWithBorders(1);
            Console.WriteLine(bottomBorder);

        }

        private void PrintArrowsAndControls()
        {
            StringBuilder upArrow = new StringBuilder();
            upArrow.Append(new string(' ', upDownArrowPosition) + Board.ArrowUp);
            upArrow.Append(new string(' ', Board.Width - upArrow.Length - 2));
            Console.WriteLine(WrapTextToBorders(upArrow.ToString()));
            Console.WriteLine(WrapTextToBorders(ControlsAndPauseLine()));

            StringBuilder downArrow = new StringBuilder();
            downArrow.Append(new string(' ', upDownArrowPosition) + Board.ArrowDown);
            downArrow.Append(new string(' ', Board.Width - downArrow.Length - 2));

            Console.WriteLine(WrapTextToBorders(downArrow.ToString()));


        }

        public string ControlsAndPauseLine()
        {
            StringBuilder controlsAndPauseLine = new StringBuilder();
            controlsAndPauseLine.Append(new string(' ', padding));
            controlsAndPauseLine.Append("Controls: " + Board.ArrowLeft + "   " + Board.ArrowRight);
            controlsAndPauseLine.Append("  Pause: [SPACE] ");
            controlsAndPauseLine.Append(new string(' ', Board.Width - controlsAndPauseLine.Length - 2));

            return controlsAndPauseLine.ToString();
        }
        private void PrintCenteredText(string text)
        {
            string centeredText = new string(' ', padding) + text + new string(' ', Board.Width - 2 - padding - text.Length);
            Console.WriteLine(WrapTextToBorders(centeredText));
        }
        public string WrapTextToBorders(string text)
        {
            return Board.VerticalLine + text + Board.VerticalLine;
        }
        public string HorizontalLinesBetweenCorners(char leftSide, char rightSide)
        {
            return leftSide + new string(Board.HorizontalLine, Board.Width - 2) + rightSide;
        }
        public void PrintEmptyLineWithBorders(int count)
        {
            for (int i = 0; i < count; i++) {
                Console.Write(Board.VerticalLine);
                Console.Write(new string(' ', Board.Width - 2));
                Console.Write(Board.VerticalLine);
                Console.WriteLine();
            }
            
        }

        public void PrintApple(Apple apple) {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.SetCursorPosition(apple.XCoord, apple.YCoord);
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
        public void PrintScore(int score)
        {
            Console.SetCursorPosition(0, scoreRow);
            PrintEmptyLineWithBorders(1);
            Console.SetCursorPosition(scoreCol, scoreRow);
            Console.Write("Score: " + score);
        }
        public void PrintPauseBanner()
        {
            string banner = Properties.Resources.PauseBanner;
            int bannerWidth = 29;
            int bannerHeight = 6;
           
            int left = (Board.Width - bannerWidth)/2;
            int top = (Board.Height - bannerHeight)/2;

                                                        // +2 for /r/n characters
            for (int i = 0; i < banner.Length; i += bannerWidth+2) {
                Console.SetCursorPosition(left, top++);
                Console.WriteLine(banner.Substring(i,bannerWidth));
            }
        }
        public void PrintDeadBanner(int score)
        {

             string banner = Properties.Resources.DeadBanner;
             int bannerWidth = 39;
             int bannerHeight = 8;

             (int left, int top) bannerPosition;
             bannerPosition.left = (Board.Width - bannerWidth) / 2;
             bannerPosition.top = (Board.Height - bannerHeight) / 2;

            for (int i = 0; i < banner.Length; i += bannerWidth + 2)
            {
                Console.SetCursorPosition(bannerPosition.left, bannerPosition.top++);
                Console.WriteLine(banner.Substring(i, bannerWidth));
            }
            (int left, int top) scorePosition = (22,5);

             Console.SetCursorPosition(bannerPosition.left + scorePosition.left, bannerPosition.top-bannerHeight+scorePosition.top-2);
             Console.Write(score);
             Console.SetCursorPosition(0, 0);
        }
        public void ClearBanner()
        {
            Console.SetCursorPosition(0, 0);
            PrintGameField();
        }
        
    }
}
