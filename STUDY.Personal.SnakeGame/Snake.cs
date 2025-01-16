using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class Snake
    {
        (int, int) startingCoords = (10, 10);
        private List<SnakeBodyPart> SnakeBody;
        private SnakeBodyPart SnakeHead;
        private SnakeBodyPart SnakeTail;
        private int SnakeLength = 1;

        public Snake()
        {
            SnakeBody = new List<SnakeBodyPart>() { new SnakeBodyPart(startingCoords) };
            SnakeHead = SnakeBody[0];
            SnakeTail = SnakeBody[SnakeLength - 1];
        }
        public void testPrint()
        {
            foreach (SnakeBodyPart sbp in SnakeBody)
            {
                Console.WriteLine(sbp.ToString());

            }
        }
    }
}
