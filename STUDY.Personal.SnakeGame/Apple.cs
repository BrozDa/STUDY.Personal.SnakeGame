namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represent an Apple in the Snake Game
    /// </summary>
    internal class Apple
    {
        private int _xCoord;
        private int _yCoord;
        private GameBoard _board;
        private Snake _snake;

        public int XCoord { get { return _xCoord; } }
        public int YCoord { get { return _yCoord; } }

        public readonly char appleCharacter = '\u00F3'; //ó

        /// <summary>
        /// Represent an Apple in the Snake Game
        /// </summary>
        /// <param name="board">GameBoard used for the game</param>
        /// <param name="snake">Snake object used for the game</param>
        public Apple(GameBoard board, Snake snake)
        {
            _board = board;
            _snake = snake;
        }
        /// <summary>
        /// Generates new apple on the board
        /// </summary>
        public void GenerateNewApple()
        {
            Random random = new Random();
            do
            {
                _xCoord = random.Next(1, _board.Width-1);
                _yCoord = random.Next(1, _board.Height-1);

            } while (!CheckIfValidPositionForApple());
        }
        /// <summary>
        /// Ensures that apple is not generated on same place with any part of the snake
        /// </summary>
        /// <returns>true if snakebody and apple do not overlap</returns>
        private bool CheckIfValidPositionForApple() {
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
