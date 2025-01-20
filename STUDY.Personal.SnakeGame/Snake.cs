using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    
    internal class Snake
    {
        private int xStartingCoord = 10;
        private int yStartingCoord = 10;

        private char headCharacter = '\u25B7';
        private char bodyCharacter = '\u25A1';

        private List<SnakeBodyPart> SnakeBody { get; set; }
        private SnakeBodyPart SnakeHead { get; set; }
        private SnakeBodyPart SnakeTail { get; set; }

        
        public Direction currentDirection { get; set; }


        private int boardWidth {get; init;}
        private int boardHeight { get; init; }
        private InputManager inputManager;



        public Snake(GameBoard board, InputManager inputManager, Direction direction)
        {
            xStartingCoord = board.Width / 2;
            yStartingCoord = board.Height / 2;
            SnakeBody = new List<SnakeBodyPart>() { new SnakeBodyPart(xStartingCoord, yStartingCoord) };
            SnakeHead = SnakeBody[0];
            SnakeTail = SnakeBody[SnakeBody.Count - 1];


            boardWidth = board.Width;
            boardHeight = board.Height;

            currentDirection = direction;   

            this.inputManager = inputManager;



        }


        public void UpdateSnake()
        {
            SnakeBodyPart newHead = CalculateNewHeadPosition(currentDirection);

            DisplayManager.RemoveTailFromScreen(SnakeTail);

            if (ValidateNewHeadPosition(newHead)) {
                SnakeBody.Insert(0, newHead);
                SnakeBody.RemoveAt(SnakeBody.Count - 1);

                SnakeHead = SnakeBody[0];
                SnakeTail = SnakeBody[SnakeBody.Count - 1];
            }
            else
            {
                Console.WriteLine("You Ded");
            }
        }
        public void UpdateSnakeDirection()
        {
            Direction newDirection = inputManager.ProcessUserInput();
            currentDirection = ValidateNewDirection(currentDirection, newDirection);
        }

        //to make sure snake cant do 180
        public Direction ValidateNewDirection(Direction currentDirection, Direction newDirection)
        {
            if(currentDirection == Direction.Up && newDirection == Direction.Down)
                newDirection = currentDirection;

            if (currentDirection == Direction.Down && newDirection == Direction.Up)
                newDirection = currentDirection;

            if (currentDirection == Direction.Left && newDirection == Direction.Right)
                newDirection = currentDirection;

            if (currentDirection == Direction.Right && newDirection == Direction.Left)
                newDirection = currentDirection;

            return newDirection;
        }
       

        public SnakeBodyPart CalculateNewHeadPosition(Direction direction)
        {
            SnakeBodyPart newHead = new SnakeBodyPart(SnakeHead.xCoord, SnakeHead.yCoord);
            switch (direction) { 
                case Direction.Up:
                    newHead.yCoord -= 1;
                    headCharacter = '\u25B3';
                    break;
                case Direction.Down:
                    newHead.yCoord += 1;
                    headCharacter = '\u25BD';
                    break;
                case Direction.Right:
                    newHead.xCoord += 1;
                    headCharacter = '\u25B7';
                    break;
                case Direction.Left:
                    newHead.xCoord -= 1;
                    headCharacter = '\u25C1';
                    break;
                
            }
            return newHead;
        }
        public bool ValidateNewHeadPosition(SnakeBodyPart newHeadPosition)
        {
            return ((newHeadPosition.xCoord > 0 && newHeadPosition.xCoord < boardWidth-1)
                && (newHeadPosition.yCoord > 0 && newHeadPosition.yCoord < boardHeight-1));

        }
        public List<SnakeBodyPart> GetSnakeBody()
        {
            return this.SnakeBody;
        }
        public char GetSnakeHeadCharacter()
        {
            return headCharacter;
        }
        public char GetSnakeBodyCharacter()
        {
            return bodyCharacter;
        }
        public SnakeBodyPart GetSnakeHead()
        {
            return this.SnakeHead;
        }
        public void AppleEaten(SnakeBodyPart newPart)
        {
            throw new NotImplementedException();
        }
        public void InsertNewHead(SnakeBodyPart newHead)
        {
            SnakeBody.Insert(0, newHead);
        }
    }
}
