namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represents game field for the snake game
    /// </summary>
    internal class GameBoard
    {
        private int _width;
        private int _height;
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public readonly char TopLeftCorner = '\u2554'; //╔
        public readonly char TopRightCorner = '\u2557'; //╗
        public readonly char BottomLeftCorner = '\u255A'; //╚
        public readonly char BottomCorner = '\u255D'; //╝
        public readonly char VerticalLineWithLeft = '\u2563'; //╣
        public readonly char VerticalLineWithRight = '\u2560'; //╠
        public readonly char VerticalLine = '\u2551'; //║
        public readonly char HorizontalLine = '\u2550'; //═ - not a equal sign
        public readonly char ArrowUp = '\u2191'; //↑
        public readonly char ArrowDown = '\u2193'; //↓
        public readonly char ArrowLeft = '\u2190'; //←
        public readonly char ArrowRight = '\u2192'; //→

        /// <summary>
        /// Represents game field for the snake game
        /// </summary>
        /// <param name="width">Integer value representing width of game field</param>
        /// <param name="height">Integer value representing height of game field</param>
        public GameBoard(int width, int height)
        {
            _width = width;
            _height = height;
        }

    }
}
