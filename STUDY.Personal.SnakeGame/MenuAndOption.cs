namespace STUDY.Personal.SnakeGame
{
    /// <summary>
    /// Represents instance of menu and option class. Object contains string array containing menu options and integer for current menu selection
    /// </summary>
    public class MenuAndOption
    {
        private bool _isSelectionMenu;
       
        public string[] MenuList { get; init; }
        public int Selection { get; set; }
        public bool IsSelectionMenu { get { return _isSelectionMenu; } }

        /// <summary>
        /// Initializes a new instance of the MenuAndOption class
        /// </summary>
        /// <param name="menu">String array representing all menu options</param>
        /// <param name="isSelectionMenu">Bool set to true if the menu is used to set setting of the game</param>
        public MenuAndOption(string[] menu, bool isSelectionMenu)
        {
            this.MenuList = menu;
            _isSelectionMenu = isSelectionMenu;
            Selection = 0;
        }
    }
}


