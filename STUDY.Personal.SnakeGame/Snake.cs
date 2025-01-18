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
            SnakeBody = [new(xStartingCoord,yStartingCoord), new(xStartingCoord-1, yStartingCoord), new(xStartingCoord-2, yStartingCoord),];
            SnakeHead = SnakeBody[0];
            SnakeTail = SnakeBody[SnakeBody.Count - 1];


            boardWidth = board.Width;
            boardHeight = board.Height;

            currentDirection = direction;   

            this.inputManager = inputManager;



        }


        public void UpdateSnake()
        {
            UpdateSnakeDirection();

            SnakeBodyPart newHead = CalculateNewHeadPosition(currentDirection);

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
            currentDirection = inputManager.ProcessUserInput();
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
            return ((newHeadPosition.xCoord > 0 && newHeadPosition.xCoord < boardWidth)
                && (newHeadPosition.yCoord > 0 && newHeadPosition.yCoord < boardHeight));

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
    }
}
