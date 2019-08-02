using System;

namespace SpaceTail
{
    class Program
    {
        static string appVersion = "0.1 ALPHA";

        static string[] devMenu_start =
        {
            "Welcome to SpaceTail!",
            "Created by KostarSf",
            "Version " + appVersion,
            "",
            "- Menu -",
            "1. Start game",
            "2. About",
            "3. Quit",
        };

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            drawConsoleMenu("devMenu");
        }

        private static void drawConsoleMenu(string menuName)
        {
            int chosedMenu;

            switch (menuName)
            {
                case "devMenu":
                    Console.Clear();
                    drawTextArray(devMenu_start);
                    chosedMenu = getIntKeyInput();
                    switch (chosedMenu)
                    {
                        case 1:
                            drawConsoleMenu("devMenu_StartGame");
                            break;
                        case 2:
                            drawConsoleMenu("devMenu_About");
                            break;
                        case 3:
                            drawConsoleMenu("devMenu_Quit");
                            break;
                        default:
                            drawConsoleMenu("devMenu");
                            break;
                    }
                    break;
                case "devMenu_Quit":
                    Console.CursorVisible = true;
                    Console.Write("\n\nFarewell! (press any key) ");
                    Console.ReadKey(true);
                    Console.WriteLine();
                    break;
                default:
                    drawConsoleMenu("devMenu");
                    break;
            }
        }

        private static int getIntKeyInput()
        {
            int inputNumber = 0;
            Console.Write("\nYour choise is.. ");
            while (int.TryParse(Console.ReadKey(false).KeyChar.ToString(), out inputNumber) != true)
            {
                Console.CursorLeft -= 1;
            }
            return inputNumber;
        }

        private static void drawTextArray(string[] textArray)
        {
            foreach (var textLine in textArray)
            {
                Console.WriteLine(textLine);
            }
        }
    }
}
