using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail
{
    class Input
    {
        public static char GetInputKeyChar(bool showCharInConsole)
        {
            return Console.ReadKey(!showCharInConsole).KeyChar;
        }

        internal static bool GetDigit(ref int outputDigit)
        {
            return int.TryParse(Input.GetInputKeyChar(true).ToString(), out outputDigit);
        }
    }
    class Screen
    {
        static string leftMargin = " ";
        public static void SimpleTextLine(string output)
        {
            Console.WriteLine(leftMargin + output);
        }
        public static void SimpleText(string output)
        {
            Console.Write(leftMargin + output);
        }
        public static void DisableCursor()
        {
            Console.CursorVisible = false;
        }
        public static void EnableCursor()
        {
            Console.CursorVisible = true;
        }
        public static void SkipLine()
        {
            Console.WriteLine();
        }
        public static void SkipLines(int linesCount)
        {
            for (int i = 1; i <= linesCount; i++)
            {
                Console.WriteLine();
            }
        }

        internal static void Clear()
        {
            Console.Clear();
        }

        public static void ColoredOutput(string line)
        {
            //"Some [red,]text that [,grn]must be [blu,gry]colored"

            string rawLine = line;
            string clearLine = "";

            Dictionary<int, string> colorParts = new Dictionary<int, string>();
            for (int index = 0; index < rawLine.Length; index++)
            {
                if (rawLine.Length > index && rawLine[index] == '[')
                {
                    if (rawLine.Length > index + 5 && rawLine[index + 5] == ']')
                    {
                        colorParts.Add(index, rawLine.Substring(index + 1, 4));
                        rawLine = rawLine.Remove(index, 6);
                        index--;
                    }
                    else if (rawLine.Length > index + 8 && rawLine[index + 8] == ']')
                    {
                        colorParts.Add(index, rawLine.Substring(index + 1, 7));
                        rawLine = rawLine.Remove(index , 9);
                        index--;
                    }
                }

            }

            StringBuilder linePart = new StringBuilder();
            for (int index = 0; index < rawLine.Length; index++)
            {
                if (colorParts.ContainsKey(index))
                {
                    Console.Write(linePart.ToString());
                    setColor(colorParts[index]);

                    linePart.Clear();
                    linePart.Append(rawLine[index]);
                }
                else
                {
                    linePart.Append(rawLine[index]);
                }

                if (index == rawLine.Length - 1)
                {
                    Console.Write(linePart.ToString());
                }
            }

            ResetColor();

            void setColor(string value)
            {
                string[] values = value.ToUpper().Split(',');

                if (values[0].Length == 3)
                {
                    Color(values[0]);
                }

                if (values[1].Length == 3)
                {
                    ColorBG(values[1]);
                }
            }

            void Color(string color)
            {
                switch (color)
                {
                    case "BLA":
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case "BLU":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "CYA":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case "DBL":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case "DCY":
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case "DGY":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case "DGN":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case "DMG":
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case "DRE":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case "DYE":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "GRA":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case "GRE":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "MAG":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case "RED":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "WHI":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "YEL":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "RST":
                        ConsoleColor backColor = Console.BackgroundColor;
                        Console.ResetColor();
                        Console.BackgroundColor = backColor;
                        break;
                }
            }

            void ColorBG(string color)
            {
                switch (color)
                {
                    case "BLA":
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    case "BLU":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case "CYA":
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        break;
                    case "DBL":
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        break;
                    case "DCY":
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        break;
                    case "DGY":
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        break;
                    case "DGN":
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;
                    case "DMG":
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case "DRE":
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;
                    case "DYE":
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "GRA":
                        Console.BackgroundColor = ConsoleColor.Gray;
                        break;
                    case "GRE":
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case "MAG":
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        break;
                    case "RED":
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case "WHI":
                        Console.BackgroundColor = ConsoleColor.White;
                        break;
                    case "YEL":
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case "RST":
                        ConsoleColor textColor = Console.ForegroundColor;
                        Console.ResetColor();
                        Console.ForegroundColor = textColor;
                        break;
                }
            }
        }

        public static void ColoredOutput(string line, bool useMargin)
        {
            if (useMargin)
            {
                ColoredOutput(leftMargin + line);
            }
        }

        public static void ColoredOutput(string[] lines)
        {
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };
            foreach (var line in lines)
            {
                ColoredOutput(line);
                Console.SetCursorPosition(cursorPos[0], ++cursorPos[1]);
            }
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }
    }
    class Program
    {
        static string appVersion = "0.1 ALPHA";

        static string[] devMenu_start =
        {
            "Welcome to [dbl,]SpaceTail[rst,]!",
            "Created by [dgy,]KostarSf",
            "Version " + appVersion,
            "",
            " - Menu -",
            "1. Start game",
            "2. About",
            "3. Quit",
        };

        static string[] devMenu_about =
        {
            "[dbl,]SpaceTail[rst,] " + appVersion,
            "Created by [dgy,]KostarSf",
            "",
            "[dgy,]A story about one pony",
            "[dgy,]that stuck alone in space.",
            "",
            " - Menu -",
            "1. Github author's page: github.com/KostarSf",
            "2. Github game's page:   kostarsf.github.io/SpaceTail",
            "3. Back",
        };

        enum Menu
        {
            DevMenu,
            DevMenu_StartGame,
            DevMenu_About,
            DevMenu_Quit,
        }

        static void Main(string[] args)
        {
            Screen.DisableCursor();
            Screen.Clear();
            //tryingInColoredLines();
            drawConsoleMenu(Menu.DevMenu);
        }

        private static void tryingInColoredLines()
        {
            String singleLine = "[blu,]Some [red,]text that [mag,grn]must be [blu,gry]colored";

            String[] multiLine = {
                "[DGN,]Some [red,]text that [mag,gre]must be [blu,gra]colored",
                "Some [blu,]text that[,gre] must be[red,gra] colored",
            };

            Console.CursorLeft = 3;
            Screen.ColoredOutput(multiLine);

        }

        private static void drawConsoleMenu(Menu menuName)
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
                            drawConsoleMenu(Menu.DevMenu_StartGame);
                            break;
                        case 2:
                            drawConsoleMenu(Menu.DevMenu_About);
                            break;
                        case 3:
                            drawConsoleMenu(Menu.DevMenu_Quit);
                            break;
                        default:
                            drawConsoleMenu(Menu.DevMenu);
                            break;
                    }
                    break;
                case Menu.DevMenu_About:
                    Console.Clear();
                    drawTextArray(devMenu_about);
                    chosedMenu = getIntKeyInput();
                    switch (chosedMenu)
                    {
                        case 1:
                            drawConsoleMenu(Menu.DevMenu_About);
                            //System.Diagnostics.Process.Start("https://github.com/KostarSf");
                            break;
                        case 2:
                            drawConsoleMenu(Menu.DevMenu_About);
                            //System.Diagnostics.Process.Start("https://kostarsf.github.io/SpaceTail");
                            break;
                        case 3:
                            drawConsoleMenu(Menu.DevMenu);
                            break;
                        default:
                            drawConsoleMenu(Menu.DevMenu_About);
                            break;
                    }
                    break;
                case Menu.DevMenu_Quit:
                    Screen.EnableCursor();
                    Screen.SkipLines(2);
                    Screen.SimpleText("Farewell! (press any key) ");
                    Console.ReadKey(true);
                    Screen.SkipLine();
                    break;
                default:
                    drawConsoleMenu(Menu.DevMenu);
                    break;
            }
        }

        private static int getIntKeyInput()
        {
            int inputNumber = 0;
            Screen.SkipLine();
            Screen.SimpleText("Your choise is.. ");
            while (Input.GetDigit(ref inputNumber) != true)
            {
                Console.CursorLeft -= 1;
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
