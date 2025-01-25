namespace STUDY.Personal.SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SnakeGame game = new SnakeGame((50,20));
            game.PlayGame();

            /*DisplayManager manageris = new DisplayManager(new GameBoard(50, 20, ' '));
            manageris.PrintPauseBanner();
            Console.ReadLine(); */
            /*MainMenu menu = new MainMenu();
            Console.ReadLine(); */
        }
    }
}
