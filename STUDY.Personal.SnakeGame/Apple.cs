using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class Apple
    {

        public int xCoord { get; set; }
        public int yCoord { get; set; }

        public char appleCharacter = '\u00F3';
        public Apple()
        {
        }

        public void GenerateNewApple(GameBoard board, Snake snake)
        {
            Random random = new Random();
            
            do
            {
                xCoord = random.Next(1, board.Width-1);
                yCoord = random.Next(1, board.Height-1);

            } while (!CheckIfValidPositionForApple(xCoord, yCoord, snake));
    
        }

        public bool CheckIfValidPositionForApple(int appleXCoord, int appleYCoord, Snake snake) {
            List<SnakeBodyPart> snakeBody = snake.GetSnakeBody();
            foreach (SnakeBodyPart sbpPart in snakeBody)
            {
                if (sbpPart.xCoord == appleXCoord && sbpPart.yCoord == appleYCoord)
                    return false;
            }
            return true;
        }
    }
}
