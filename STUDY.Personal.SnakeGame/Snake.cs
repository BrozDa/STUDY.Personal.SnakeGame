namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represent a Snake object which used for the Snake Game
    /// </summary>
    internal class Snake
    {
        
        private char _headCharacter; 
        private char _bodyCharacter = '\u25A1'; //□
        private readonly GameBoard _board;
        private readonly bool _canMoveThroughWalls;
        private List<SnakeBodyPart> _snakeBody;
        private SnakeBodyPart _snakeHead;
        private SnakeBodyPart _snakeTail;
        private Direction _currentDirection;

        public char HeadCharacter { get { return _headCharacter; } }
        public char BodyCharacter { get { return _bodyCharacter; } }
        public List<SnakeBodyPart> SnakeBody { get { return _snakeBody; } }
        public SnakeBodyPart SnakeHead { get { return _snakeHead; } }
        public SnakeBodyPart SnakeTail { get { return _snakeTail; } }
        public Direction CurrentDirection { get { return _currentDirection; } }

        /// <summary>
        /// Initializes a new instance of the Snake class
        /// </summary>
        /// <param name="board"><see cref="GameBoard"/> object representing the game field</param>
        /// <param name="direction"><see cref="Direction"/> value representing default direction</param>
        /// <param name="canMoveThroughWalls"><see cref="bool"/> value detemining if snake can walk through walls</param>
        public Snake(GameBoard board, Direction direction, bool canMoveThroughWalls)
        {
            _board = board;
            _canMoveThroughWalls = canMoveThroughWalls;
            _snakeBody = new List<SnakeBodyPart>() { new SnakeBodyPart(board.Width / 2, board.Height / 2) };
            _snakeHead = _snakeBody[0];
            _snakeTail = _snakeBody[0];
            _currentDirection = direction;
            UpdateSnakeHeadCharacter(direction);
        }
        /// <summary>
        /// Checks whether apple has been eaten and update the snake accordingly
        /// </summary>
        /// <param name="newHead"><see cref="SnakeBodyPart"/> object representing a new head</param>
        /// <param name="appleEaten"><see cref="bool"/> value determining if apple has been eaten</param>
        public void UpdateSnake(SnakeBodyPart newHead, bool appleEaten)
        {
            //only valid position are being passed from GameTick
            if (appleEaten)
            {
                _snakeBody.Insert(0, newHead);
            }
            else
            {
                _snakeBody.Insert(0, newHead);
                _snakeBody.RemoveAt(_snakeBody.Count - 1);
                _snakeTail = _snakeBody[_snakeBody.Count - 1];
            }
            _snakeHead = newHead;

        }
        /// <summary>
        /// Validates <see cref="Direction"/> taken from user input and update it accordingly
        /// </summary>
        /// <param name="newDirection"><see cref="Direction"/> value representing new direction</param>
        public void UpdateSnakeDirection(Direction newDirection)
        {
            _currentDirection = ValidateNewDirection(_currentDirection, newDirection);
        }
        /// <summary>
        /// Validates <see cref="Direction"/> value taken from user input and validates it in a way that snake cannot perform 180 turn in one input
        /// </summary>
        /// <param name="currentDirection"><see cref="Direction"/> value representing current direction of the snake</param>
        /// <param name="newDirection"><see cref="Direction"/> value representing new direction taken from user input</param>
        /// <returns><see cref="Direction"/> value representing valid direction, if invalid <see cref="Direction"/> remains the same</returns>
        private Direction ValidateNewDirection(Direction currentDirection, Direction newDirection)
        {
            if(currentDirection == Direction.Up && newDirection == Direction.Down)
            {
                newDirection = currentDirection;
            } 
            else if (currentDirection == Direction.Down && newDirection == Direction.Up)
            {
                newDirection = currentDirection;
            }
            else if(currentDirection == Direction.Left && newDirection == Direction.Right)
            {
                newDirection = currentDirection;
            }
            else if(currentDirection == Direction.Right && newDirection == Direction.Left)
            {
                newDirection = currentDirection;
            }
            return newDirection;
        }
        /// <summary>
        /// Retrieve new <see cref="SnakeBodyPart"/> object representing new head based on passed direction
        /// </summary>
        /// <param name="direction"><see cref="Direction"/>value representing the direction of a snake</param>
        /// <returns><see cref="SnakeBodyPart"/> object representing new head based on passed direction</returns>
        public SnakeBodyPart GetNewHeadObject(Direction direction)
        {
            SnakeBodyPart newHead = new SnakeBodyPart(SnakeHead.xCoord, SnakeHead.yCoord);

            switch (direction) 
            { 
                case Direction.Up:
                    newHead.yCoord -= 1;
                    break;
                case Direction.Down:
                    newHead.yCoord += 1;
                    break;
                case Direction.Right:
                    newHead.xCoord += 1;
                    break;
                case Direction.Left:
                    newHead.xCoord -= 1;
                    break;
            }
            return newHead;
        }
        /// <summary>
        /// Updates <see cref="SnakeBodyPart"/> head object character based on passed direction
        /// </summary>
        /// <param name="direction"><see cref="Direction"/>value representing the direction of a snake</param>
        public void UpdateSnakeHeadCharacter(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    _headCharacter = '\u25B3';
                    break;
                case Direction.Down:
                    _headCharacter = '\u25BD';
                    break;
                case Direction.Right:
                    _headCharacter = '\u25B7';
                    break;
                case Direction.Left:
                    _headCharacter = '\u25C1';
                    break;
            }
        }
        /// <summary>
        /// Validates whether <see cref="SnakeBodyPart"/>  object representing snake head position is valid - if snake is still alive
        /// </summary>
        /// <param name="newHeadPosition"><see cref="SnakeBodyPart"/>  object representing new snake head position</param>
        /// <returns><see cref="bool"/> true if the position is valid, false otherwise</returns>
        public bool ValidateNewHeadPosition(SnakeBodyPart newHeadPosition)
        {
            return NotInWall(newHeadPosition) && NotEatenHimself(newHeadPosition);
        }
        /// <summary>
        /// Checks whether <see cref="Snake"/> head is not in wall, updates it accordingly if snake can move through walls
        /// </summary>
        /// <param name="newHeadPosition"><see cref="SnakeBodyPart"/>  object representing new snake head position</param>
        /// <returns><see cref="bool"/> true if the snake is not in wall, false otherwise</returns>
        private bool NotInWall(SnakeBodyPart newHeadPosition)
        {
            if (_canMoveThroughWalls)
            {
                TeleportToOtherSideIfInWall(newHeadPosition);
                return true;
            }
            else
            {
                return ((newHeadPosition.xCoord > 0 && newHeadPosition.xCoord < _board.Width - 1)
                && (newHeadPosition.yCoord > 0 && newHeadPosition.yCoord < _board.Height - 1));
            }
        }
        /// <summary>
        /// whether <see cref="Snake"/> head is not in wall, if so then moves it to the other side of the game field
        /// </summary>
        /// <param name="newHeadPosition"><see cref="SnakeBodyPart"/>  object representing new snake head position</param>
        private void TeleportToOtherSideIfInWall(SnakeBodyPart newHeadPosition)
        {
            if(newHeadPosition.xCoord == 0)
            {
                newHeadPosition.xCoord = _board.Width - 2;
            }
            else if (newHeadPosition.yCoord == 0)
            {
                newHeadPosition.yCoord = _board.Height - 2;
            }
            else if (newHeadPosition.xCoord == _board.Width - 1)
            {
                newHeadPosition.xCoord = 1;
            }
            else if (newHeadPosition.yCoord == _board.Height - 1)
            {
                newHeadPosition.yCoord = 1;
            }
        }
        /// <summary>
        /// Compares Snake head position against rest of the snake to check if it did not eat himself
        /// </summary>
        /// <param name="newHeadPosition"></param>
        /// <returns><see cref="bool"/> true if the snake did not eat himself, false otherwise</returns>
        private bool NotEatenHimself(SnakeBodyPart newHeadPosition)
        {
            foreach(SnakeBodyPart bodyPart in _snakeBody)
            {
                if (bodyPart.Equals(newHeadPosition))
                    return false;
            }
            return true;
        }
    }
}
