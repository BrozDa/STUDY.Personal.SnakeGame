using System;
using System.ComponentModel.Design;

namespace STUDY.Personal.SnakeGame
{
    public class MenuAndOption
    {

        public string[] menu;
        public int option = 0;
        public bool isSelectionMenu;

        public MenuAndOption(string[] menu, bool isSelectionMenu)
        {
            this.menu = menu;
            this.isSelectionMenu = isSelectionMenu;
        }
    }
}


