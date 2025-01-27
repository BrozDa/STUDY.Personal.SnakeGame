namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represents Main Menu and settings for the Snake Game
    /// </summary>
    internal class MainMenu
    {
        
        private readonly string snakeAscii = Properties.Resources.SnakeAscii;

        private readonly string[] _mainMenu = {"  << Start a new game >>", "  << Settings >>", "  << Exit >>" };
        private readonly string[] _settingMenu = { "  << Size >>", "  << Moving Through Walls >>  ", "  << Exit to main menu >>  " };
        private readonly string[] _sizeMenu = { "  << Small >>", "  << Medium >>  ", "  << Large >>  ", "  << Exit >>  " };
        private readonly string[] _movingThroughWallsMenu = { "  << Enabled >>", "  << Disabled >>  ", "  << Exit >>  " };
        private Stack<MenuAndOption> _menuQueue = new Stack<MenuAndOption>();
        private List<MenuAndOption> _menuList = new List<MenuAndOption>();
        private Dictionary<int, (int left, int top)> _sizeList = new Dictionary<int, (int left, int top)>();
        private int _currentMenu = 0;
        private (int left, int top) _mainMenuOptionLocation = (0, 0); //position where menu should be printed, properly initialized in PrintLogo method
        private (int width, int height) _size = (50, 20);
        private bool _canMoveThroughWalls = false;

        /// <summary>
        /// Initializes a new instance of the Main Menu class
        /// </summary>
        public MainMenu()
        {
            InitializeMenuList();
            InitializeSizeList();
            Console.CursorVisible = false;
        }
        /// <summary>
        /// Initializes MenuList with all of its current options
        /// </summary>
        private void InitializeMenuList()
        {
            _menuList.Add(new MenuAndOption(_mainMenu, false));
            _menuList.Add(new MenuAndOption(_settingMenu, false));
            _menuList.Add(new MenuAndOption(_sizeMenu, true));
            _menuList.Add(new MenuAndOption(_movingThroughWallsMenu, true));
        }
        /// <summary>
        /// Initializes SizeList with all of its current options
        /// </summary>
        private void InitializeSizeList()
        {
            _sizeList.Add(0, (42, 20));
            _sizeList.Add(1, (60, 20));
            _sizeList.Add(2, (100, 20));
        }
        /// <summary>
        /// Prints the Main menu and starts awaiting input from the user
        /// </summary>
        public void StartMenu()
        {
            PrintLogo();
            PrintMenu(_menuList[0]);
            ProcessMenu(_menuList);
        }
        /// <summary>
        /// Prints logo and initial text
        /// </summary>
        public void PrintLogo()
        {
            Console.WriteLine(snakeAscii);
            Console.WriteLine();
            Console.WriteLine("Welcome to Snake Game");
            Console.WriteLine("Use Up and Down arrow keys to shift through the menu and then press Enter to select one");
            Console.WriteLine();
            _mainMenuOptionLocation = Console.GetCursorPosition();
        }
        /// <summary>
        /// Prints current menu and highlight selection, it the menu is selection menu then it highligths current setting
        /// </summary>
        /// <param name="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        public void PrintMenu(MenuAndOption currentMenu)
        {
            int currentMenuOption = currentMenu.option;
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top);
            ClearPreviousMenu();
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top);

            for (int i = 0; i < currentMenu.menu.Count(); i++) {
                Console.WriteLine(currentMenu.menu[i]);
            }

            HighLightMenuItem(currentMenu);
            if (currentMenu.isSelectionMenu) {
                HighLightSelection(currentMenu);
            }
        }
        /// <summary>
        /// Clearing previous menu from the screen
        /// </summary>
        public void ClearPreviousMenu()
        {
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top);
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
            Console.WriteLine(new string(' ', 30));
        }
        /// <summary>
        /// Highlights item user choose using arrows keys
        /// </summary>
        /// <param name="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        private void HighLightMenuItem(MenuAndOption currentMenu)
        {
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top+ currentMenu.option);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(currentMenu.menu[currentMenu.option]);
            Console.ResetColor();
        }
        /// <summary>
        /// Removes highlighs from item from which user went away
        /// </summary>
        /// <param name="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        private void UnHighLightMenuItem(MenuAndOption menu, bool seletion = false)
        {
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top+ menu.option);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(menu.menu[menu.option]);
            Console.ResetColor();
        }
        /// <summary>
        /// Highlights current setting in menus which could be used to change said settings
        /// </summary>
        /// <param currentMenu="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        public void HighLightSelection(MenuAndOption currentMenu)
        {
            int optionToHighlight = currentMenu.option;
            //size
            if (currentMenu.menu == _menuList[2].menu)
            {
                //optionToHightlight is key for value which is already stored in _size
                optionToHighlight = _sizeList.FirstOrDefault(x => x.Value == _size).Key;
            }
            //walkable walls
            if (currentMenu.menu == _menuList[3].menu)
            {
                optionToHighlight = (_canMoveThroughWalls ? 1 : 0);
            }
            Console.SetCursorPosition(_mainMenuOptionLocation.left, _mainMenuOptionLocation.top + optionToHighlight);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(currentMenu.menu[optionToHighlight]);
            Console.ResetColor();
        }
        /// <summary>
        /// Gets user input which and performing action based on the input
        /// </summary>
        /// <param name="menuList">List of all available menus in the menu tree</param>
        public void ProcessMenu(List<MenuAndOption> menuList)
        {
            ConsoleKey key;
            MenuAndOption previousMenu = menuList[_currentMenu];
            MenuAndOption newMenu = menuList[_currentMenu];

            while (true) {
                key = Console.ReadKey().Key;
                newMenu = ProcessUserInput(newMenu, key);

                if (newMenu.menu != previousMenu.menu) {
                    PrintMenu(newMenu);
                }
                else if (newMenu.isSelectionMenu)
                {
                    HighLightSelection(newMenu);
                }
                previousMenu = newMenu;
            } 
        }
        /// <summary>
        /// Perform action based on user input passed as parameter
        /// </summary>
        /// <param name="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        /// <param name="key">Key user pressed</param>
        /// <returns>Instance of MenuAndOption representing current menu being processed</returns>
        private MenuAndOption ProcessUserInput(MenuAndOption currentMenu, ConsoleKey key)
        {
            int maxMenuOption = currentMenu.menu.Length -1;

            if (key == ConsoleKey.DownArrow)
            {
                UnHighLightMenuItem(currentMenu);
                currentMenu.option = (currentMenu.option == maxMenuOption) ? (currentMenu.option - maxMenuOption) : (currentMenu.option + 1);
                HighLightMenuItem(currentMenu);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                UnHighLightMenuItem(currentMenu);
                currentMenu.option = (currentMenu.option == 0) ? (currentMenu.option + maxMenuOption) : (currentMenu.option - 1);
                HighLightMenuItem(currentMenu);
            }
            else if(key == ConsoleKey.Enter) {
                HighLightSelection(currentMenu);
                currentMenu = ProcessCurrentMenuSelection(currentMenu);
                PrintMenu(currentMenu);
            }
            return currentMenu;
        }
        /// <summary>
        /// Perform actions based on current menu being processed
        /// </summary>
        /// <param name="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        /// <returns>Instance of MenuAndOption updated based on user input</returns>
        private MenuAndOption ProcessCurrentMenuSelection(MenuAndOption currentMenu)
        {
            
            //main menu
            if (currentMenu.menu == _menuList[0].menu)
            {
                currentMenu = ProcessMainMenu(currentMenu);
            }
            //settings menu
            else if (currentMenu == _menuList[1])
            {
                currentMenu = ProcessSettingsMenu(currentMenu);
            }
            //size
            else if(currentMenu == _menuList[2]) {

                currentMenu = ProcessSizeMenu(currentMenu);
            }
            //walkable walls
            else if(currentMenu == _menuList[3])
            {
                currentMenu = ProcessWalkingThroughWallsMenu(currentMenu);
            }
            return currentMenu;
        }
        /// <summary>
        /// Perform action in the main menu based option user selected
        /// </summary>
        /// <param currentMenu="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        /// <returns>Instance of MenuAndOption updated based on user input</returns>
        private MenuAndOption ProcessMainMenu(MenuAndOption currentMenu)
        {
            switch (currentMenu.option)
            {
                case 0:
                    PlayGame();
                    break;
                case 1:
                    _menuQueue.Push(currentMenu);
                    currentMenu = _menuList[1];//settings
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
            return currentMenu;
        }
        /// <summary>
        /// Perform action in the settins menu based option user selected
        /// </summary>
        /// <param currentMenu="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        /// <returns>Instance of MenuAndOption updated based on user input</returns>
        private MenuAndOption ProcessSettingsMenu(MenuAndOption currentMenu)
        {
            switch (currentMenu.option)
            {
                case 0:
                    _menuQueue.Push(currentMenu);
                    currentMenu = _menuList[2]; //size
                    break;
                case 1:
                    _menuQueue.Push(currentMenu);
                    currentMenu = _menuList[3]; //walkable walls
                    break;
                case 2:
                    currentMenu = _menuQueue.Pop();
                    break;

            }
            return currentMenu;
        }
        /// <summary>
        /// Perform action in the size menu based option user selected
        /// </summary>
        /// <param currentMenu="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        /// <returns>Instance of MenuAndOption updated based on user input</returns>
        private MenuAndOption ProcessSizeMenu(MenuAndOption currentMenu)
        {
            if (currentMenu.option <= 2)
            {
                _size = (_sizeList[currentMenu.option].left, _sizeList[currentMenu.option].top);
            }
            else
            {
                currentMenu = _menuQueue.Pop();
            }
            return currentMenu;
        }
        /// <summary>
        /// Perform action in the walking through walls menu based option user selected
        /// </summary>
        /// <param currentMenu="currentMenu">Instance of MenuAndOption representing current menu being processed</param>
        /// <returns>Instance of MenuAndOption updated based on user input</returns>
        private MenuAndOption ProcessWalkingThroughWallsMenu(MenuAndOption currentMenu)
        {
            switch (currentMenu.option)
            {
                case 0:
                    _canMoveThroughWalls = false;
                    break;
                case 1:
                    _canMoveThroughWalls = true;

                    break;
                case 2:
                    currentMenu = _menuQueue.Pop();
                    break;
            }
            return currentMenu;
        }
        /// <summary>
        /// Prints out the logo, menu and starts processing the menu
        /// </summary>
        public void NewGame() {
            Console.Clear();
            StartMenu();
        }
        /// <summary>
        /// Starts a new game using current game settings
        /// </summary>
        private void PlayGame()
        {
            Console.Clear();
            SnakeGame game = new SnakeGame(_size, _canMoveThroughWalls);
            game.PlayGame();
            NewGame();

        }
          
           











      


    }
}
