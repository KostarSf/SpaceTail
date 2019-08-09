using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail.Game
{
    class ConsoleMenu
    {
        static string[] devMenu_start =
        {
            "Welcome to [DarkBlue,]SpaceTail[Reset,]!",
            "Created by [DarkGray,]KostarSf",
            "Version " + SpaceTail.appVersion,
            "",
            " - Menu -",
            "1. Start game",
            "2. About",
            "3. Quit",
        };

        static string[] devMenu_about =
        {
            "[DarkBlue,]SpaceTail[Reset,] " + SpaceTail.appVersion,
            "Created by [DarkGray,]KostarSf",
            "",
            "[DarkGray,]A story about one pony",
            "[DarkGray,]that stuck alone in space.",
            "",
            " - Menu -",
            "1. Github author's page: github.com/KostarSf",
            "2. Github game's page:   kostarsf.github.io/SpaceTail",
            "3. Back",
        };

        public enum Menu
        {
            DevMenu,
            DevMenu_StartGame,
            DevMenu_About,
            DevMenu_Quit,
        }
        public static void DrawConsoleMenu(Menu menuName)
        {
            int chosedMenu;

            switch (menuName)
            {
                case Menu.DevMenu:
                    Console.Clear();
                    drawTextArray(devMenu_start);
                    chosedMenu = getIntKeyInput();
                    switch (chosedMenu)
                    {
                        case 1:
                            DrawConsoleMenu(Menu.DevMenu_StartGame);
                            break;
                        case 2:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            break;
                        case 3:
                            DrawConsoleMenu(Menu.DevMenu_Quit);
                            break;
                        default:
                            DrawConsoleMenu(Menu.DevMenu);
                            break;
                    }
                    break;

                case Menu.DevMenu_StartGame:
                    Game.StartGame();
                    break;

                case Menu.DevMenu_About:
                    Console.Clear();
                    drawTextArray(devMenu_about);
                    chosedMenu = getIntKeyInput();
                    switch (chosedMenu)
                    {
                        case 1:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            //System.Diagnostics.Process.Start("https://github.com/KostarSf");
                            break;
                        case 2:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            //System.Diagnostics.Process.Start("https://kostarsf.github.io/SpaceTail");
                            break;
                        case 3:
                            DrawConsoleMenu(Menu.DevMenu);
                            break;
                        default:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            break;
                    }
                    break;

                case Menu.DevMenu_Quit:
                    Screen.ShowCursor();
                    Screen.SkipLines(2);
                    Screen.SimpleText("Farewell! (press any key) ");
                    Console.ReadKey(true);
                    Screen.SkipLine();
                    break;

                default:
                    DrawConsoleMenu(Menu.DevMenu);
                    break;
            }
        }

        private static int getIntKeyInput()
        {
            int inputNumber = 0;
            int[] inputPos = { Console.CursorLeft, Console.CursorTop };

            Screen.SkipLine();
            Screen.SimpleText("Your choise is.. ");

            while (Input.GetDigit(ref inputNumber) != true)
            {
                Console.SetCursorPosition(inputPos[0], inputPos[1]);
            }

            return inputNumber;
        }

        private static void drawTextArray(string[] textArray)
        {
            foreach (var textLine in textArray)
            {
                Screen.ColoredOutput(textLine + "\n", true);
            }
        }
    }
}
