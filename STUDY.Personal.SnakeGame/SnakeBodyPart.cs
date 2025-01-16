using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class SnakeBodyPart
    {
        (int x , int y) BodyPart;

        public SnakeBodyPart((int, int) coords)
        {
            BodyPart = coords;
        }
        public override string ToString()
        {
            return new string($"x: {BodyPart.x}, y: {BodyPart.y}");
        }
    }
}
