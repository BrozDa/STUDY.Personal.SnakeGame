using System.Text;

namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represent object managing all output to the screen
    /// </summary>
    internal class DisplayManager
    {
        private readonly string _pressEnterText = ">>> Press [ENTER] to start <<<";
        private readonly int _scoreRow;
        private readonly int _scoreCol;
        private GameBoard _board;
        private int _padding;
        private string _controlsAndPauseLine;
        private int _upDownArrowPositionFromLeft;
        /// <summary>
        /// Initializes new instance of DisplayManager class
        /// </summary>
        /// <param name="board">GameBoard used for the game</param>
        public DisplayManager(GameBoard board)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _board = board;
            _padding = (_board.Width - 2 - _pressEnterText.Length) / 2;
            _controlsAndPauseLine = GetControlsAndPauseLine();
            _upDownArrowPositionFromLeft = _controlsAndPauseLine.IndexOf(_board.ArrowLeft) + 2;
            _scoreRow = board.Height + 1;
            _scoreCol = 1 + _padding + 12;
        }
        /// <summary>
        /// Prints out the gamefield and supporting menu
        /// </summary>
        public void PrintGame()
        {
            string bottomBorder = GetHorizontalLineBetweenCorners(_board.BottomLeftCorner, _board.BottomCorner);

            PrintGameField();
            PrintEmptyLineWithBorders(1);
            PrintPressEnterText();
            PrintEmptyLineWithBorders(1);
            PrintArrowsAndControls();
            PrintEmptyLineWithBorders(1);
            Console.WriteLine(bottomBorder);
        }
        /// <summary>
        /// Prints out gamefield for the snake
        /// </summary>
        private void PrintGameField()
        {
            string topBorder = GetHorizontalLineBetweenCorners(_board.TopLeftCorner, _board.TopRightCorner);
            string middleBorder = GetHorizontalLineBetweenCorners(_board.VerticalLineWithRight, _board.VerticalLineWithLeft);

            Console.WriteLine(topBorder);
            PrintEmptyLineWithBorders(_board.Height - 2);
            Console.WriteLine(middleBorder);
        }
        /// <summary>
        /// Returns text with an arrow on its designated location
        /// </summary>
        /// <param name="arrow">character representing the arrow</param>
        /// <returns>Empty line with arrow on its designated position</returns>
        private string GetStringWithArrow(char arrow)
        {
            StringBuilder upArrow = new StringBuilder();
            upArrow.Append(new string(' ', _upDownArrowPositionFromLeft) + arrow);
            upArrow.Append(new string(' ', _board.Width - upArrow.Length - 2));

            return upArrow.ToString();
        }
        /// <summary>
        /// Returns text with control, arrow and pause text
        /// </summary>
        /// <returns>Empty line with control, arrow and pause text on their designated positions</returns>
        private string GetControlsAndPauseLine()
        {
            StringBuilder controlsAndPauseLine = new StringBuilder();
            controlsAndPauseLine.Append(new string(' ', _padding));
            controlsAndPauseLine.Append("Controls: " + _board.ArrowLeft + new string(' ', 3) + _board.ArrowRight);
            controlsAndPauseLine.Append("  Pause: [SPACE] ");
            controlsAndPauseLine.Append(new string(' ', _board.Width - controlsAndPauseLine.Length - 2));

            return controlsAndPauseLine.ToString();
        }
        /// <summary>
        /// Returns horizontal line in between corner characters
        /// </summary>
        /// <param name="leftSide">Left corner character</param>
        /// <param name="rightSide">Right corner character</param>
        /// <returns></returns>
        private string GetHorizontalLineBetweenCorners(char leftSide, char rightSide)
        {
            return leftSide + new string(_board.HorizontalLine, _board.Width - 2) + rightSide;
        }
        /// <summary>
        /// Returns text with press enter to start 
        /// </summary>
        /// <returns>Empty line with enter to start text on its designated positions</returns>
        private string GetPressEnterLine()
        {
            StringBuilder pressEnterLine = new StringBuilder();
            pressEnterLine.Append(new string(' ', _padding) + _pressEnterText);
            pressEnterLine.Append(new string(' ', _padding));

            return pressEnterLine.ToString();
        }
        /// <summary>
        /// Returns string with border characters on its side
        /// </summary>
        /// <param name="text">Text that needs to be wrapped in border characters</param>
        /// <returns>Text wrapped in border characters</returns>
        private string WrapTextToBorders(string text)
        {
            return _board.VerticalLine + text + _board.VerticalLine;
        }
        /// <summary>
        /// Prints section containing contoll arrows and pause text
        /// </summary>
        private void PrintArrowsAndControls()
        {
            string upArrowLine = GetStringWithArrow(_board.ArrowUp);
            string downArrowLine = GetStringWithArrow(_board.ArrowDown);

            PrintEmptyLineWithBorders(1);
            Console.WriteLine(WrapTextToBorders(upArrowLine));
            Console.WriteLine(WrapTextToBorders(_controlsAndPauseLine));
            Console.WriteLine(WrapTextToBorders(downArrowLine));
        }
        /// <summary>
        /// Prints out the line with Press Enter Text in borders
        /// </summary>
        private void PrintPressEnterText()
        {
            Console.WriteLine(WrapTextToBorders(GetPressEnterLine()));
        }
        /// <summary>
        /// Prints empty lines wrapped in border characters 
        /// </summary>
        /// <param name="count">Represent number of emptry lines</param>
        private void PrintEmptyLineWithBorders(int count)
        {
            for (int i = 0; i < count; i++) 
            {
                Console.Write(_board.VerticalLine);
                Console.Write(new string(' ', _board.Width - 2));
                Console.Write(_board.VerticalLine);
                Console.WriteLine();
            }   
        }
        /// <summary>
        /// Prints out the apple on the board in red color
        /// </summary>
        /// <param name="apple">Object representing the apple</param>
        public void PrintApple(Apple apple) 
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.SetCursorPosition(apple.XCoord, apple.YCoord);
            Console.Write(apple.AppleCharacter);
            Console.ResetColor();   
        }
        /// <summary>
        /// Prints the snake on the screen
        /// </summary>
        /// <param name="snake">Snake which should be printed</param>
        public void PrintSnake(Snake snake) 
        {
            List<SnakeBodyPart> snakeBody = snake.SnakeBody;

            //snake head
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(snakeBody[0].XCoord, snakeBody[0].YCoord);
            Console.Write(snake.HeadCharacter);

            //rest of the body
            for (int i = 1; i < snakeBody.Count; i++)
            {
                Console.SetCursorPosition(snakeBody[i].XCoord, snakeBody[i].YCoord);
                Console.Write(snake.BodyCharacter);
            }
            Console.ResetColor();
        }
        /// <summary>
        /// Removes snake tail from the screen
        /// </summary>
        /// <param name="tail">SnakeBodyPart object representing snake's tail</param>
        public void RemoveTailFromScreen(SnakeBodyPart tail)
        {
            Console.SetCursorPosition(tail.XCoord, tail.YCoord);
            Console.Write(" ");
        }
        /// <summary>
        /// Prints current score to the screen on its designated location
        /// </summary>
        /// <param name="score">Integer representing currrent score</param>
        public void PrintScore(int score)
        {
            //replace current score from the screen
            Console.SetCursorPosition(0, _scoreRow);
            PrintEmptyLineWithBorders(1);
            //print updated score
            Console.SetCursorPosition(_scoreCol, _scoreRow);
            Console.Write("Score: " + score);
        }
        /// <summary>
        /// Prints banner representing pause to the screen
        /// </summary>
        public void PrintPauseBanner()
        {
            string banner = Properties.Resources.PauseBanner;
            int bannerWidth = 29; //manually set based on the string in properties
            int bannerHeight = 6; //manually set based on the string in properties
            (int left, int top) bannerPosition;
            bannerPosition.left = (_board.Width - bannerWidth) / 2;
            bannerPosition.top = (_board.Height - bannerHeight) / 2;

                                          // +2 for /r/n characters
            for (int i = 0; i < banner.Length; i += bannerWidth+2) {
                Console.SetCursorPosition(bannerPosition.left, bannerPosition.top);
                bannerPosition.top++;
                Console.WriteLine(banner.Substring(i,bannerWidth));
            }
        }
        /// <summary>
        /// Prints banner after dying including achieved score
        /// </summary>
        /// <param name="score">Integer representing achieved score</param>
        public void PrintDeadBanner(int score)
        {
             string banner = Properties.Resources.DeadBanner;
             int bannerWidth = 39;  // manually set based on the string in properties
             int bannerHeight = 8;  // manually set based on the string in properties
             (int left, int top) bannerPosition;
             bannerPosition.left = (_board.Width - bannerWidth) / 2;
             bannerPosition.top = (_board.Height - bannerHeight) / 2;
                                                    // +2 for /r/n characters
            for (int i = 0; i < banner.Length; i += bannerWidth + 2)
            {
                Console.SetCursorPosition(bannerPosition.left, bannerPosition.top);
                bannerPosition.top++;
                Console.WriteLine(banner.Substring(i, bannerWidth));
            }
            bannerPosition.top -= bannerHeight; // setting back to default
            (int left, int top) scorePositionInBanner = (22,5);

             Console.SetCursorPosition(bannerPosition.left + scorePositionInBanner.left, bannerPosition.top+scorePositionInBanner.top-2);
             Console.Write(score);
        }
        /// <summary>
        /// Clears any banner from the field
        /// </summary>
        public void ClearBanner()
        {
            Console.SetCursorPosition(0, 0);
            PrintGameField();
        }
        
    }
}
