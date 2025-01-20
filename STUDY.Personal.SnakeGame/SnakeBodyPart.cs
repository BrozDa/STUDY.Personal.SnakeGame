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
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            SnakeBodyPart? other = obj as SnakeBodyPart;
            if(other == null) return false;

            return Equals(other);
        }
        private bool Equals(SnakeBodyPart other) { 
           
            return this.xCoord == other.xCoord && this.yCoord == other.yCoord;
        }
    }
}
