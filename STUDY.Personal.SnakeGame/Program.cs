namespace STUDY.Personal.SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SnakeGame game = new SnakeGame((50,20));
            game.PlayGame();
            Console.ReadLine(); 
            /*MainMenu menu = new MainMenu();
            Console.ReadLine(); */
        }
    }
}
