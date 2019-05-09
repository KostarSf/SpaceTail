using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceTail
{
    class MenuScene : Scene
    {
        string blank16 = "                ";
        string[] spriteMenuTitle = {
            " _____                 _____     _ _ ",
            "|   __|___ ___ ___ ___|_   _|___|_| |",
            "|__   | . | .'|  _| -_| | | | .'| | |",
            "|_____|  _|__,|___|___| |_| |__,|_|_|",
            "      |_|                            ",
        };
        string[] spriteMenuSubTitle = {
            "                     _                 ",
            "_// _    __/  _     (_      _          ",
            "//)(-  _) /()/ (/ ()/  ()/)(-  /)()/)(/",
            "               /              /      / ",
        };

        List<MenuItem> menuItems = new List<MenuItem>();

        public MenuScene(Interface gameInterface)
        {
            setSceneBorders(gameInterface.getBorders());
        }

        public void setMenuItemAttributes(bool isSelected)
        {
            MenuItem item = menuItems.Last<MenuItem>();
            menuItems.Remove(item);

            item.SetSelected(isSelected);

            menuItems.Add(item);
        }

        public void setMenuItemAttributes(bool isSelected, bool isActive)
        {
            MenuItem item = menuItems.Last<MenuItem>();
            menuItems.Remove(item);

            item.SetSelected(isSelected);
            item.SetActive(isActive);

            menuItems.Add(item);
        }

        public void setMenuItemAttributes(bool isSelected, bool isActive, bool isColored)
        {
            MenuItem item = menuItems.Last<MenuItem>();
            menuItems.Remove(item);

            item.SetSelected(isSelected);
            item.SetActive(isActive);
            item.SetColored(isColored);

            menuItems.Add(item);
        }

        public void addMenuItem(MenuItem item)
        {
            menuItems.Add(item);
        }

        public void addVoidMenuItem()
        {
            menuItems.Add(new MenuItem());
        }

        internal void addTextItem(string text)
        {
            menuItems.Add(new MenuItem(text));
        }

        public List<MenuItem> GetMenuItems() => menuItems;

        public void Show()
        {
            fillScreen(" ");
            drawMenu();
            drawCenteredTopSprite(spriteMenuTitle, 0);
            drawCenteredTopSprite(spriteMenuSubTitle, 4, " ");
            drawText(Program.version, getBorder(Side.Bottom), getBorder(Side.Right) - Program.version.Length, false);
            drawText($" by {Program.author}", getBorder(Side.Bottom), getBorder(Side.Left), false);

            updateMenuList();
        }

        private void drawMenu()
        {
            drawMenuSprite(generateMenuList(), menuItems, 0, 4);
        }

        private void updateMenuList()
        {
            int selectedItem = 0;

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                
                foreach (MenuItem item in menuItems)
                {
                    if (item.IsSelected())
                    {
                        selectedItem = menuItems.IndexOf(item);
                    }
                }

                if (key == ConsoleKey.Enter
                    || key == ConsoleKey.Z)
                {
                    AudioManager.PlaySound("MenuPress");
                    break;
                }

                if (key == ConsoleKey.Escape
                    || key == ConsoleKey.X)
                {
                    selectedItem = menuItems.IndexOf(menuItems.Last<MenuItem>());
                    AudioManager.PlaySound("MenuPress");
                    break;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    menuItems[selectedItem].SetSelected(false);

                    do
                    {
                        selectedItem++;
                        if (selectedItem >= menuItems.Count)
                        {
                            selectedItem = 0;
                        }
                    } while (menuItems[selectedItem].IsSkipable() || !menuItems[selectedItem].IsActive());
                    
                    menuItems[selectedItem].SetSelected(true);
                    AudioManager.PlaySound("MenuNav");
                }

                if (key == ConsoleKey.UpArrow)
                {
                    menuItems[selectedItem].SetSelected(false);
                    do
                    {
                        if (selectedItem <= 0)
                        {
                            selectedItem = menuItems.Count - 1;
                        }
                        else
                        {
                            selectedItem--;
                        }
                    } while (menuItems[selectedItem].IsSkipable() || !menuItems[selectedItem].IsActive());

                    menuItems[selectedItem].SetSelected(true);
                    AudioManager.PlaySound("MenuNav");

                }

                drawMenu();
            }

            menuItems[selectedItem].OpenLink();
        }

        private string[] generateMenuList()
        {
            int sideOffset = 8;
            string sideChar = "*";

            List<string> menuList = new List<string>();

            int maxMenuItemTextLength = 1;

            foreach (MenuItem item in menuItems)
            {
                if (!item.IsSkipable())
                {

                    if (item.GetMenuItemText().Length > maxMenuItemTextLength)
                    {
                        maxMenuItemTextLength = item.GetMenuItemText().Length;
                    }
                }
            }

            foreach (MenuItem item in menuItems)
            {
                string menuItemText = item.GetMenuItemText();
                string menuItemOffset = "";
                string menuItemSideChar = " ";

                string menuItemFinal;

                if (menuItemText.Length % 2 == 0)
                {
                    menuItemText += " ";
                }

                for (int i = 0; i < sideOffset + (maxMenuItemTextLength - menuItemText.Length) / 2; i++)
                {
                    menuItemOffset += " ";
                }

                if (item.IsSelected())
                {
                    menuItemSideChar = sideChar;
                }

                menuItemFinal = menuItemSideChar + menuItemOffset + menuItemText + menuItemOffset + menuItemSideChar;

                if (item.IsSkipable())
                {
                    menuItemFinal = menuItemText;
                }

                menuList.Add(menuItemFinal);
            }

            return menuList.ToArray();
        }

        
    }
}
