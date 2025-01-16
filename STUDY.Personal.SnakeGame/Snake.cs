using System;
using System.Collections.Generic;
using System.Linq;
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
        private int SnakeLength = 1;
        public Direction currentDirection = Direction.Up;
        


        public Snake(GameBoard board)
        {
            SnakeBody = [new(xStartingCoord,yStartingCoord), new(xStartingCoord-1, yStartingCoord), new(xStartingCoord-2, yStartingCoord),];
            SnakeHead = SnakeBody[0];
            SnakeTail = SnakeBody[SnakeBody.Count - 1];
        }

        public void PrintSnake(char _snakeCharacter)
        {
            //new variables for head and tail
            SnakeBodyPart newHead = CalculateNewHeadPosition(currentDirection);
            SnakeBodyPart currentTail = SnakeTail;
            // remove tail from screen
            Console.SetCursorPosition(SnakeTail.xCoord, SnakeTail.yCoord);
            Console.Write(" ");

            //add new head to SnakeBody
            MoveSnake(newHead);

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
        public void MoveSnake(SnakeBodyPart newHeadPosition)
        {
            
            SnakeBody.Insert(0, newHeadPosition);
            SnakeBody.RemoveAt(SnakeBody.Count - 1);
            SnakeHead = newHeadPosition;
           
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
    }
}
