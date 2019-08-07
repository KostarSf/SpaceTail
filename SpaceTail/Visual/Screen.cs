using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceTail
{
    class Screen
    {
        public enum TextAlign
        {
            Left = 0,
            Center = 1,
            Right = 2,
        }

        public enum ScreenAlign
        {
            TopLeft,
            TopCenter,
            TopRight,

            BottomLeft,
            BottomCenter,
            BottomRight,

            MiddleLeft,
            MiddleCenter,
            MiddleRight,
        }

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

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetWindowSize(70, 20);
                Console.SetBufferSize(70, 20);
                Console.SetWindowSize(70, 20);  // Убирает отступ от скролла
            }

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
        public static void HideCursor()
        {
            Console.CursorVisible = false;
        }
        public static void ShowCursor()
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
            if (Console.WindowHeight > 0)
            {
                Console.Clear();
            }
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

        internal static void DrawLine(int x, int y, object line)
        {
            if (x < Console.BufferWidth && y < Console.BufferHeight)
            {
                Console.SetCursorPosition(x, y);
                if (line.ToString().Length <= Console.BufferWidth - x)
                {
                    Console.Write(line);
                }
                else
                {
                    int lineLength = Console.BufferWidth - x;
                    if (lineLength < line.ToString().Length)
                    {
                        Console.Write(line.ToString().Substring(0, lineLength));
                    }
                }
            }
        }
    }
}
