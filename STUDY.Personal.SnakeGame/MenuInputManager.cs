using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    internal class MenuInputManager
    {
        ConsoleKey userInput = ConsoleKey.None;


        public void ProcessInput()
        {
            userInput = Console.ReadKey().Key;
            

            
        }
    }
}
