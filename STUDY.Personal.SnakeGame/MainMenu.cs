using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace STUDY.Personal.SnakeGame
{
    
    internal class MainMenu
    {
        /*public struct MenuAndOption
        {
            public string[] menu;
            public int option = 0;

            public MenuAndOption(string[] menu)
            {
                this.menu = menu;

            }
        }*/
        private readonly string snakeAscii = @"
           _________         _________
          /         \       /         \
         /  /~~~~~\  \     /  /~~~~~\  \
         |  |     |  |     |  |     |  |
         |  |     |  |     |  |     |  |
         |  |     |  |     |  |     |  |         /
         |  |     |  |     |  |     |  |       //
        (o  o)    \  \_____/  /     \  \_____/ /
         \__/      \         /       \        /
          |         ~~~~~~~~~         ~~~~~~~~
          ^";
        MenuInputManager menuInputManager;
        private (int left, int top) mainMenuOptionLocation = (0,0);
        private readonly string[] mainMenu = new string[] {"  << Start a new game >>", "  << Settings >>", "  << Exit >>" };
        private readonly string[] settingMenu = new string[] { "  << Size >>", "  << Moving Through Walls >>  ", "  << Exit to main menu >>  " };
        private readonly string[] sizeMenu = { "  << Small >>", "  << Medium >>  ", "  << Large >>  ", "  << Exit >>  " };
        private readonly string[] movingThroughWallMenu = { "  << Enabled >>", "  << Disabled >>  ", "  << Exit >>  " };
        private int mainMenuOption = 0;

        public List<MenuAndOption> menus;
        private int currentMenu = 0;
        bool selection = false;
       

        private int currentOption = 0;
        ConsoleKey key = ConsoleKey.None;
        Stack<MenuAndOption> menuQueue = new Stack<MenuAndOption>();

        private (int width, int height) Size = (42,20);
        private bool movingThroughWall = false;

        public MainMenu()
        {
            InitializeMenus();
            //menuInputManager = new MenuInputManager();
            Console.CursorVisible = false;
            StartMenu();
        }
        public void InitializeMenus()
        {
            menus = new List<MenuAndOption>() { 
                new MenuAndOption(mainMenu, false) ,
                new MenuAndOption(settingMenu, false),
                new MenuAndOption(sizeMenu, true),
                new MenuAndOption(movingThroughWallMenu, true),

            };
        }
        public void StartMenu()
        {
            PrintLogo();
            PrintMenu(menus[0]);
            ProcessMenu(menus);

            //ProcessMenu(mainMenu, mainMenuOption);
        }

        public void PrintLogo()
        {
            Console.WriteLine(snakeAscii);
            Console.WriteLine();
            Console.WriteLine("Welcome to Snake Game");
            Console.WriteLine("Use Up and Down arrow keys to shift through the menu and then press Enter to select one");
            Console.WriteLine();
            mainMenuOptionLocation = Console.GetCursorPosition();
        }
        public void PrintMenu(MenuAndOption menu)
        {
            int currentMenuOption = menu.option;
            Console.SetCursorPosition(mainMenuOptionLocation.left, mainMenuOptionLocation.top);
            ClearPreviousMenu();
            Console.SetCursorPosition(mainMenuOptionLocation.left, mainMenuOptionLocation.top);

            for (int i = 0; i < menu.menu.Count(); i++) {
                Console.WriteLine(menu.menu[i]);
            }

            HighLightMenuItem(menu);
            if (menu.isSelectionMenu) {
                HighLightSelection(menu);
            }
        }
        
        public void ClearPreviousMenu()
        {
            Console.SetCursorPosition(mainMenuOptionLocation.left, mainMenuOptionLocation.top);
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
        }
        private void HighLightMenuItem(MenuAndOption menu)
        {
            Console.SetCursorPosition(mainMenuOptionLocation.left, mainMenuOptionLocation.top+ menu.option);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menu.menu[menu.option]);
            Console.ResetColor();
        }
        private void UnHighLightMenuItem(MenuAndOption menu, bool seletion = false)
        {
            if(seletion) {return; }

            Console.SetCursorPosition(mainMenuOptionLocation.left, mainMenuOptionLocation.top+ menu.option);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(menu.menu[menu.option]);
            Console.ResetColor();
        }
        public void ProcessMenu(List<MenuAndOption> menus)
        {
            ConsoleKey key;
            MenuAndOption previousMenu = menus[currentMenu];
            MenuAndOption newMenu = menus[currentMenu];

            while (true) {
                key = Console.ReadKey().Key;
                newMenu = RotateOptions(newMenu, key);
                if (newMenu.menu != previousMenu.menu)
                    PrintMenu(newMenu);
                else if(newMenu.isSelectionMenu)
                    HighLightSelection(newMenu);

                previousMenu = newMenu;
            }
            
        }

        private MenuAndOption RotateOptions(MenuAndOption menu, ConsoleKey key)
        {
            int maxMenuOption = menu.menu.Length -1;
            if (key == ConsoleKey.DownArrow)
            {
                UnHighLightMenuItem(menu, selection);
                if(selection)
                    selection = false;  
                if (menu.option == maxMenuOption)
                    menu.option -= maxMenuOption;
                else
                    menu.option++;

                HighLightMenuItem(menu);

            }
            if (key == ConsoleKey.UpArrow)
            {
                UnHighLightMenuItem(menu, selection);
                if (selection)
                    selection = false;

                if (menu.option == 0)
                    menu.option += maxMenuOption;
                else
                    menu.option--;
                HighLightMenuItem(menu);
            }
            
            
            if(key == ConsoleKey.Enter) {
                HighLightSelection(menu);
                menu = ProcessCurrentMenuSelection(menu);
                PrintMenu(menu);
                
            }
            return menu;
        }
        private MenuAndOption ProcessCurrentMenuSelection(MenuAndOption menu)
        {

            //main menu
            if (menu.menu == menus[0].menu)
            {
                switch (menu.option) { 
                    case 0:
                        StartNewGame();
                        selection = false;
                        //might need to clear resources
                        break;
                    case 1:
                        menuQueue.Push(menu);
                        menu = menus[1];
                        selection = false;
                        break;
                    case 2:
                        Environment.Exit(0);
                        selection = false;
                        break;
                }
            }
            //settings menu
           else if (menu == menus[1])
            {
                switch (menu.option)
                {
                    case 0:
                        menuQueue.Push(menu);
                        menu = menus[2];
                        selection = false;
                        break;
                    case 1:
                        menuQueue.Push(menu);
                        menu = menus[3];
                        selection = false;
                        break;
                    case 2:
                        menu = menuQueue.Pop();
                        selection = false;
                        break;

                }
            }
            //size
            else if(menu == menus[2]) {
                switch (menu.option)
                {
                    case 0:
                        this.Size = (50, 20);
                        selection = true;
                        break;
                    case 1:
                        this.Size = (60, 20);
                        selection = true;
                        break;
                    case 2:
                        this.Size = (100, 20);
                        selection = true;
                        break;
                    case 3:
                        menu = menuQueue.Pop();
                        selection = false;

                        break;
                }

            }
            //walkable walls
            else if(menu == menus[3])
            {
                switch (menu.option)
                {
                    case 0:
                        movingThroughWall = false;
                        selection = true;
                        break;
                    case 1:
                        movingThroughWall = true;
                        selection = true;

                        break;
                    case 2:
                        menu = menuQueue.Pop();
                        selection = false;
                        break;
                }
            }
            return menu;


        }
        public void HighLightSelection(MenuAndOption menu)
        {
            int temp= menu.option;
            //size
            if(menu.menu == menus[2].menu)
            {
                switch (Size) {
                    case (50, 20): temp = 0; break;
                    case (60, 20): temp = 1; break;
                    case (100, 20): temp = 2; break;

                }
            }
            //walls
            if (menu.menu == menus[3].menu){
                temp = (movingThroughWall? 1 : 0);   
            }
            Console.SetCursorPosition(mainMenuOptionLocation.left, mainMenuOptionLocation.top + temp);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menu.menu[temp]);
            Console.ResetColor();
        }
        public void StartNewGame() {
            Console.Clear();

            SnakeGame game;
                game = new SnakeGame(Size);
                game.PlayGame();
            if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                Console.Clear();
                PrintLogo();
                PrintMenu(menus[0]);
                ProcessMenu(menus);
            }
            else
            {
                Environment.Exit(0);
            }

            

        }
          
           











      


    }
}
