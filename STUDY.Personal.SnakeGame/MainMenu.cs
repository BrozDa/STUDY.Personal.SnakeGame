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
        private readonly string snakeAscii = Properties.Resources.SnakeAscii;

        //non backed
        private readonly string[] _mainMenu = new string[] {"  << Start a new game >>", "  << Settings >>", "  << Exit >>" };
        private readonly string[] _settingMenu = new string[] { "  << Size >>", "  << Moving Through Walls >>  ", "  << Exit to main menu >>  " };
        private readonly string[] _sizeMenu = { "  << Small >>", "  << Medium >>  ", "  << Large >>  ", "  << Exit >>  " };
        private readonly string[] _movingThroughWallsMenu = { "  << Enabled >>", "  << Disabled >>  ", "  << Exit >>  " };
        private (int left, int top) _mainMenuOptionLocation = (0, 0);
        private List<MenuAndOption> _menuList;
        private int _currentMenu = 0;
        private Stack<MenuAndOption> _menuQueue = new Stack<MenuAndOption>();
        
        private bool _canMoveThroughWalls = false;
        bool selection = false;

        private (int width, int height) Size = (42, 20);

        public MainMenu()
        {
            _menuList = new List<MenuAndOption>() {
                new MenuAndOption(_mainMenu, false) ,
                new MenuAndOption(_settingMenu, false),
                new MenuAndOption(_sizeMenu, true),
                new MenuAndOption(_movingThroughWallsMenu, true)};
            Size = (42, 20);
            Console.CursorVisible = false;
            StartMenu();
        }
        public void StartMenu()
        {
            PrintLogo();
            PrintMenu(_menuList[0]);
            ProcessMenu(_menuList);

            //ProcessMenu(mainMenu, mainMenuOption);
        }

        public void PrintLogo()
        {
            Console.WriteLine(snakeAscii);
            Console.WriteLine();
            Console.WriteLine("Welcome to Snake Game");
            Console.WriteLine("Use Up and Down arrow keys to shift through the menu and then press Enter to select one");
            Console.WriteLine();
            _mainMenuOptionLocation = Console.GetCursorPosition();
        }
        public void PrintMenu(MenuAndOption menu)
        {
            int currentMenuOption = menu.option;
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top);
            ClearPreviousMenu();
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top);

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
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top);
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
        }
        private void HighLightMenuItem(MenuAndOption menu)
        {
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top+ menu.option);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menu.menu[menu.option]);
            Console.ResetColor();
        }
        private void UnHighLightMenuItem(MenuAndOption menu, bool seletion = false)
        {
            if(seletion) {return; }

            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top+ menu.option);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(menu.menu[menu.option]);
            Console.ResetColor();
        }
        public void ProcessMenu(List<MenuAndOption> menus)
        {
            ConsoleKey key;
            MenuAndOption previousMenu = menus[_currentMenu];
            MenuAndOption newMenu = menus[_currentMenu];

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
            if (menu.menu == _menuList[0].menu)
            {
                switch (menu.option) { 
                    case 0:
                        StartNewGame();
                        selection = false;
                        //might need to clear resources
                        break;
                    case 1:
                        _menuQueue.Push(menu);
                        menu = _menuList[1];
                        selection = false;
                        break;
                    case 2:
                        Environment.Exit(0);
                        selection = false;
                        break;
                }
            }
            //settings menu
           else if (menu == _menuList[1])
            {
                switch (menu.option)
                {
                    case 0:
                        _menuQueue.Push(menu);
                        menu = _menuList[2];
                        selection = false;
                        break;
                    case 1:
                        _menuQueue.Push(menu);
                        menu = _menuList[3];
                        selection = false;
                        break;
                    case 2:
                        menu = _menuQueue.Pop();
                        selection = false;
                        break;

                }
            }
            //size
            else if(menu == _menuList[2]) {
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
                        menu = _menuQueue.Pop();
                        selection = false;

                        break;
                }

            }
            //walkable walls
            else if(menu == _menuList[3])
            {
                switch (menu.option)
                {
                    case 0:
                        _canMoveThroughWalls = false;
                        selection = true;
                        break;
                    case 1:
                        _canMoveThroughWalls = true;
                        selection = true;

                        break;
                    case 2:
                        menu = _menuQueue.Pop();
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
            if(menu.menu == _menuList[2].menu)
            {
                switch (Size) {
                    case (50, 20): temp = 0; break;
                    case (60, 20): temp = 1; break;
                    case (100, 20): temp = 2; break;

                }
            }
            //walls
            if (menu.menu == _menuList[3].menu){
                temp = (_canMoveThroughWalls ? 1 : 0);   
            }
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top + temp);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menu.menu[temp]);
            Console.ResetColor();
        }
        public void StartNewGame() {
            Console.Clear();

            SnakeGame game;
                game = new SnakeGame(Size, _canMoveThroughWalls);
                game.PlayGame();
            if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                Console.Clear();
                PrintLogo();
                PrintMenu(_menuList[0]);
                ProcessMenu(_menuList);
            }
            else
            {
                Environment.Exit(0);
            }

            

        }
          
           











      


    }
}
