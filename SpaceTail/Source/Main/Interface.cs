using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceTail
{
    static class Interface
    {
        struct Border
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;

            public void setBorders(int l, int r, int t, int b)
            {
                Left = l;
                Right = r;
                Top = t;
                Bottom = b;
            }

            public int[] getBorders()
            {
                int[] borders = { Left, Right, Top, Bottom };
                return borders;
            }
        }

        static Border GameBorder = new Border();

        public static int[] getBorders()
        {
            return GameBorder.getBorders();
        }

        public static void Initialize()
        {
            Console.Clear();
            Console.CursorVisible = false;

            setGameBorders();
            drawFrameBorders();
            ClearGameField();
        }

        static void setGameBorders()
        {
            GameBorder.setBorders(3, Console.WindowWidth - 2, 2, Console.WindowHeight - 3);
        }

        static void drawFrameBorders()
        {
            for (int i = 2; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("═");
            }

            for (int i = 2; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write("═");
            }

            for (int i = 2; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(2, i);
                Console.Write("║");
            }

            for (int i = 2; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(2, 1);
            Console.Write("╔");

            Console.SetCursorPosition(Console.WindowWidth - 1, 1);
            Console.Write("╗");

            Console.SetCursorPosition(2, Console.WindowHeight - 2);
            Console.Write("╚");

            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 2);
            Console.Write("╝");
        }

        public static void ClearGameField()
        {
            for (int i = GameBorder.Top; i <= GameBorder.Bottom; i++)
            {
                for (int j = GameBorder.Left; j <= GameBorder.Right; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }
        }

        public static void DrawCenteredSprite(string[] sprite)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1;
            int topStartPoint = Console.WindowHeight / 2 - sprite.Length / 2;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > GameBorder.Top && i < GameBorder.Bottom
                        && j > GameBorder.Left && j < GameBorder.Right)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        public static void DrawCenteredOffsetSprite(string[] sprite, int offsetX, int offsetY)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1 + offsetX;
            int topStartPoint = Console.WindowHeight / 2 - sprite.Length / 2 + offsetY;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > GameBorder.Top && i < GameBorder.Bottom
                        && j > GameBorder.Left && j < GameBorder.Right)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        public static void DrawMenuSprite(string[] sprite, List<MenuItem> menuItems, int offsetX, int offsetY)
        {
            int leftStartPoint;
            int topStartPoint = Console.WindowHeight / 2 - sprite.Length / 2 + offsetY;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                leftStartPoint = Console.WindowWidth / 2 - sprite[row].Length / 2 + 1 + offsetX;

                if (!menuItems[row].IsActive())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (menuItems[row].IsColored())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > GameBorder.Top && i < GameBorder.Bottom
                        && j > GameBorder.Left && j < GameBorder.Right)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        public static void DrawCenteredTopSprite(string[] sprite, int topStart)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1;
            int topStartPoint = GameBorder.Top + topStart;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i >= GameBorder.Top && i < GameBorder.Bottom
                        && j > GameBorder.Left && j < GameBorder.Right)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        public static void DrawCenteredTopSprite(string[] sprite, int topStart, string voidchar)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1;
            int topStartPoint = GameBorder.Top + topStart;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > GameBorder.Top && i < GameBorder.Bottom
                        && j > GameBorder.Left && j < GameBorder.Right)
                    {
                        if (sprite[row][col] != voidchar[0])
                        {
                            Console.SetCursorPosition(j, i);
                            Console.Write(sprite[row][col]);
                        }
                    }
                    col++;
                }
                row++;
            }
        }

        public static void DrawText(string text, int XStart, int YStart, bool invertAxis)
        {
            int XStartPoint = GameBorder.Left + XStart;
            int YStartPoint = GameBorder.Top + YStart;

            if (invertAxis)
            {
                XStartPoint = GameBorder.Right - XStart - text.Length;
                YStartPoint = GameBorder.Bottom - YStart;
            }

            Console.SetCursorPosition(XStartPoint, YStartPoint);
            Console.Write(text);
        }

        public static void DrawText(string text, int XStart, int YStart, bool invertXAxis, bool invertYAxis)
        {
            int XStartPoint = GameBorder.Left + XStart;
            int YStartPoint = GameBorder.Top + YStart;

            if (invertXAxis)
            {
                XStartPoint = GameBorder.Right - XStart - text.Length;
            }

            if (invertYAxis)
            {
                YStartPoint = GameBorder.Bottom - YStart;
            }

            Console.SetCursorPosition(XStartPoint, YStartPoint);
            Console.Write(text);
        }

        public static void FillScene(string bg)
        {
            for (int i = GameBorder.Top; i <= GameBorder.Bottom; i++)
            {
                for (int k = GameBorder.Left; k <= GameBorder.Right; k++)
                {
                    Console.SetCursorPosition(k, i);
                    Console.Write(bg);
                }
            }
        }

        public static void FillPatternScreen(string bg1, string bg2, bool oddOffset)
        {
            for (int i = GameBorder.Top; i <= GameBorder.Bottom; i++)
            {
                for (int k = GameBorder.Left; k < GameBorder.Right; k += 2)
                {
                    Console.SetCursorPosition(k, i);
                    if (oddOffset && i % 2 == 0)
                    {
                        Console.Write(bg2 + bg1);
                    }
                    else
                    {
                        Console.Write(bg1 + bg2);
                    }
                }
            }
        }

        public static void DrawTransition(string bg, string edge, int speed)
        {
            for (int i = GameBorder.Left; i <= GameBorder.Right; i++)
            {
                for (int j = GameBorder.Top; j <= GameBorder.Bottom; j++)
                {
                    Console.SetCursorPosition(i, j);

                    if (i == GameBorder.Right)
                        Console.Write($"{bg}");
                    else
                        Console.Write($"{bg}{edge}");
                }

                Thread.Sleep(speed);
            }
        }
    }
}
