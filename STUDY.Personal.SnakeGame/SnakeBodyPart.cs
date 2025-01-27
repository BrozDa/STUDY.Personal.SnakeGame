namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represent single element in the snake body
    /// </summary>
    internal class SnakeBodyPart
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        /// <summary>
        /// Initializes new instance of SnakeBodyPart class
        /// </summary>
        /// <param name="xCoord"><see cref="int"/> value representing x coordinate</param>
        /// <param name="yCoord"><see cref="int"/> value representing y coordinate</param>
        public SnakeBodyPart(int xCoord, int yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
        }
        /// <summary>
        /// Compares current <see cref="SnakeBodyPart"/> object against other and checks whether they are located on same coordinates
        /// </summary>
        /// <param name="obj"><see cref="object"/> instance for comparison</param>
        /// <returns><see cref="bool"/> true if coordinates are same, false if not or passed object is null or not of <see cref="SnakeBodyPart"/>instance</returns>
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            SnakeBodyPart? other = obj as SnakeBodyPart;
            if(other == null) return false;

            return Equals(other);
        }
        /// <summary>
        /// Compares X and Y coordinates of two <see cref="SnakeBodyPart"/> objects
        /// </summary>
        /// <param name="other">Other <see cref="SnakeBodyPart"/> for comparison</param>
        /// <returns><see cref="bool"/> true if coordinates are same</returns>
        private bool Equals(SnakeBodyPart other) { 
           
            return this.XCoord == other.XCoord && this.YCoord == other.YCoord;
        }
        /// <summary>
        /// Strictly to get rid of CS0659 compiler warning
        /// </summary>
        /// <returns>Always <see cref="int"/> 0</returns>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
