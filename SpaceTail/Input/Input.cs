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

        public static string GetEmptyLine(int length)
        {
            var line = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                line.Append(' ');
            }
            return line.ToString();
        }

        internal static int Average(List<int> numbers)
        {
            int result = 0;
            foreach (var number in numbers)
            {
                result += number;
            }
            return result / numbers.Count;
        }

        internal static List<int> PutInts(int number, int count)
        {
            var result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(number);
            }
            return result;
        }

        public static string PutChars(char @char, int count)
        {
            var result = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                result.Append(@char);
            }
            return result.ToString();
        }

        internal static ConsoleColor GetDefaultForegroundColor()
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;

            Console.ResetColor();
            ConsoleColor foregroundColor = Console.ForegroundColor;

            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;

            return foregroundColor;
        }

        internal static ConsoleColor GetDefaultBackgroundColor()
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;

            Console.ResetColor();
            ConsoleColor backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;

            return backgroundColor;
        }

        internal static string ReverseLine(string line)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= line.Length; i++)
            {
                result.Append(line[line.Length - i]);
            }

            return result.ToString();
        }
    }
}
