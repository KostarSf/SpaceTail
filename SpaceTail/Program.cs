using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceTail
{
    class Game
    {
        static Task inputCycle;
        static int inputCount = 0;

        static bool gameIsRunning;
        static int[] consoleSizes = { Console.WindowWidth, Console.WindowHeight };

        static int fps = 30;
        static int targetCycleTime = 1000 / fps;

        static DateTime time;

        public static void StartGame()
        {
            gameIsRunning = true;
            inputCount = 0;
            Screen.Clear();

            Stopwatch cycleTimer = new Stopwatch();
            int currentCycleTime;
            int cycleCounter = 0;

            Stopwatch fpsTimer = new Stopwatch();

            Console.SetBufferSize(consoleSizes[0], consoleSizes[1]);

            inputCycle = new Task(UpdateInput);
            inputCycle.Start();

            while (gameIsRunning)
            {
                fpsTimer.Restart();
                cycleTimer.Restart();

                //UpdateLogic();
                DrawFrame();

                cycleTimer.Stop();
                currentCycleTime = (int)cycleTimer.ElapsedMilliseconds;

                if (currentCycleTime < targetCycleTime)
                {
                    Thread.Sleep(targetCycleTime - currentCycleTime);
                }

                Console.SetCursorPosition(0, 0);
                cycleCounter++;
                switch (cycleCounter)
                {
                    case 1:
                        Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  -   ");
                        break;
                    case 2:
                        Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  \   ");
                        break;
                    case 3:
                        Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  |   ");
                        break;
                    case 4:
                        Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  /   ");
                        cycleCounter = 0;
                        break;
                }
            }

            inputCycle.Wait();
            inputCycle.Dispose();
            Program.DrawConsoleMenu(Program.Menu.DevMenu);
        }

        private static void UpdateInput()
        {
            ConsoleKeyInfo pressedKey;

            while (gameIsRunning)
            {
                pressedKey = Console.ReadKey(true);
                inputCount++;

                if (pressedKey.Key == ConsoleKey.Q)
                {
                    gameIsRunning = false;
                }
            }
        }

        private static void DrawFrame()
        {
            Screen.DisableCursor();
            try
            {
                Console.SetWindowSize(consoleSizes[0], consoleSizes[1]);
                Console.SetBufferSize(consoleSizes[0], consoleSizes[1]);

                Console.SetCursorPosition(0, 1);
                Console.Write($"Pressed keys count: {inputCount}");
                Console.SetCursorPosition(0, 2);
                Console.Write($"'Q' to quit");
            } catch (Exception e)
            {
                Screen.Clear();
            }
        }
    }
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
        public static Dictionary<string, ConsoleColor> Colors;

        static string leftMargin = " ";

        public static void Init()
        {
            Colors = new Dictionary<string, ConsoleColor>();
            Colors.Add("BLACK",         ConsoleColor.Black);
            Colors.Add("BLUE",          ConsoleColor.Blue);
            Colors.Add("CYAN",          ConsoleColor.Cyan);
            Colors.Add("DARKBLUE",      ConsoleColor.DarkBlue);
            Colors.Add("DARKCYAN",      ConsoleColor.DarkCyan);
            Colors.Add("DARKGRAY",      ConsoleColor.DarkGray);
            Colors.Add("DARKGREEN",     ConsoleColor.DarkGreen);
            Colors.Add("DARKMAGENTA",   ConsoleColor.DarkMagenta);
            Colors.Add("DARKRED",       ConsoleColor.DarkRed);
            Colors.Add("DARKYELLOW",    ConsoleColor.DarkYellow);
            Colors.Add("GRAY",          ConsoleColor.Gray);
            Colors.Add("GREEN",         ConsoleColor.Green);
            Colors.Add("MAGENTA",       ConsoleColor.Magenta);
            Colors.Add("RED",           ConsoleColor.Red);
            Colors.Add("WHITE",         ConsoleColor.White);
            Colors.Add("YELLOW",        ConsoleColor.Yellow);

            Console.SetWindowSize(70, 20);
            Console.SetBufferSize(70, 20);

            Console.Title = "SpaceTail";
        }

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
            StringBuilder colorKeys = new StringBuilder();

            Dictionary<int, string> colorParts = new Dictionary<int, string>();

            for (int index = 0; index < rawLine.Length; index++)
            {
                if (rawLine[index] == '[')
                {
                    colorKeys.Clear();
                    int i = 0;
                    while (rawLine[index + i++ + 1] != ']')
                    {
                        colorKeys.Append(rawLine[index + i]);
                    }
                    colorParts.Add(index, colorKeys.ToString());

                    rawLine = rawLine.Remove(index, i + 1);
                    index--;
                }
            }

            StringBuilder linePart = new StringBuilder();
            for (int index = 0; index < rawLine.Length; index++)
            {
                if (colorParts.ContainsKey(index))
                {
                    Console.Write(linePart.ToString());
                    setColorFromParseValue(colorParts[index]);

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

            void setColorFromParseValue(string value)
            {
                string[] colors = value.Split(',');

                SetColors(colors[0].Trim(), colors[1].Trim());
            }

            void SetColors(string fgColor, string bgColor)
            {
                fgColor = fgColor.Trim().ToUpper();
                bgColor = bgColor.Trim().ToUpper();

                if (Colors.ContainsKey(fgColor))
                {
                    Console.ForegroundColor = Colors[fgColor];
                }
                else if (fgColor == "RESET")
                {
                    ConsoleColor lastBg = Console.BackgroundColor;
                    Console.ResetColor();
                    Console.BackgroundColor = lastBg;
                }

                if (Colors.ContainsKey(bgColor))
                {
                    Console.BackgroundColor = Colors[bgColor];
                }
                else if (bgColor == "RESET")
                {
                    ConsoleColor lastFg = Console.ForegroundColor;
                    Console.ResetColor();
                    Console.ForegroundColor = lastFg;
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
            "Welcome to [DarkBlue,]SpaceTail[Reset,]!",
            "Created by [DarkGray,]KostarSf",
            "Version " + appVersion,
            "",
            " - Menu -",
            "1. Start game",
            "2. About",
            "3. Quit",
        };

        static string[] devMenu_about =
        {
            "[DarkBlue,]SpaceTail[Reset,] " + appVersion,
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

        static void Main(string[] args)
        {
            Screen.Init();
            Screen.DisableCursor();
            Screen.Clear();
            DrawConsoleMenu(Menu.DevMenu);
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
                    Screen.EnableCursor();
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
            Screen.SkipLine();
            Screen.SimpleText("Your choise is.. ");
            int[] inputPos = { Console.CursorLeft, Console.CursorTop };
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
