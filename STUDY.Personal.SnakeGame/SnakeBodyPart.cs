using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class SnakeBodyPart
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }

        public SnakeBodyPart(int x, int y)
        {
            xCoord = x;
            yCoord = y;
        }
        public override string ToString()
        {
            return new string($"x: {xCoord}, y: {yCoord}");
        }
    }
}
