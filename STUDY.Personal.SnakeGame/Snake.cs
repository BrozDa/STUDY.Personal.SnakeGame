using System;
using System.Collections.Generic;
using System.Linq;
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
            boardWidth = board._width;
            boardHeight = board._height;
            currentDirection = direction;   
            this.inputManager = inputManager;


        }
        public void UpdateSnake()
        {

        }
        public void PrintSnake(char _snakeCharacter)
        {
            currentDirection = inputManager.ProcessUserInput();

            //new variables for head and tail
            SnakeBodyPart newHead = CalculateNewHeadPosition(currentDirection);
            SnakeBodyPart currentTail = SnakeTail;
            // remove tail from screen
            Console.SetCursorPosition(SnakeTail.xCoord, SnakeTail.yCoord);
            Console.Write(" ");

            //add new head to SnakeBody
            UpdateHead(newHead);

            // update tail:
            SnakeTail = SnakeBody[SnakeBody.Count - 1];

            //print head
            Console.SetCursorPosition(SnakeHead.xCoord, SnakeHead.yCoord);
            Console.Write(headCharacter);

            //print rest of the snake
            for (int i = 1; i < SnakeBody.Count; i++) { 
                int x = SnakeBody[i].xCoord;
                int y = SnakeBody[i].yCoord;
                Console.SetCursorPosition(x, y);
                Console.Write(bodyCharacter);
            }

        }
        public void UpdateHead(SnakeBodyPart newHeadPosition)
        {
            if (ValidateNewHeadPosition(newHeadPosition)){
                SnakeBody.Insert(0, newHeadPosition);
                SnakeBody.RemoveAt(SnakeBody.Count - 1);
                SnakeHead = newHeadPosition;
            }
            else
            {
                Console.WriteLine("YA DED");
            }
            
           
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
    }
}
