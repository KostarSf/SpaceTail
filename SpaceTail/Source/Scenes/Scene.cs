using System;
using System.Collections.Generic;

namespace SpaceTail
{
    class Scene
    {
        int[] sceneBorders;

        internal enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public Scene()
        {

        }

        internal void setSceneBorders(int[] borders)
        {
            sceneBorders = borders;
        }

        internal int getBorder(Side side)
        {
            return sceneBorders[(int)side];
        }

        internal void drawCenteredSprite(string[] sprite)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1;
            int topStartPoint = Console.WindowHeight / 2 - sprite.Length / 2;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > getBorder(Side.Top) && i < getBorder(Side.Bottom)
                        && j > getBorder(Side.Left) && j < getBorder(Side.Right))
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        internal void drawCenteredOffsetSprite(string[] sprite, int offsetX, int offsetY)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1 + offsetX;
            int topStartPoint = Console.WindowHeight / 2 - sprite.Length / 2 + offsetY;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > getBorder(Side.Top) && i < getBorder(Side.Bottom)
                        && j > getBorder(Side.Left) && j < getBorder(Side.Right))
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        internal void drawMenuSprite(string[] sprite, List<MenuItem> menuItems, int offsetX, int offsetY)
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
                    if (i > getBorder(Side.Top) && i < getBorder(Side.Bottom)
                        && j > getBorder(Side.Left) && j < getBorder(Side.Right))
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        internal void drawCenteredTopSprite(string[] sprite, int topStart)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1;
            int topStartPoint = getBorder(Side.Top) + topStart;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i >= getBorder(Side.Top) && i < getBorder(Side.Bottom)
                        && j > getBorder(Side.Left) && j < getBorder(Side.Right))
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(sprite[row][col]);
                    }
                    col++;
                }
                row++;
            }
        }

        internal void drawCenteredTopSprite(string[] sprite, int topStart, string voidchar)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2 + 1;
            int topStartPoint = getBorder(Side.Top) + topStart;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                int col = 0;
                for (int j = leftStartPoint; j < leftStartPoint + sprite[row].Length; j++)
                {
                    if (i > getBorder(Side.Top) && i < getBorder(Side.Bottom)
                        && j > getBorder(Side.Left) && j < getBorder(Side.Right))
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

        internal void drawText(string text, int topStart, int leftStart, bool relative)
        {
            int leftStartPoint = leftStart;
            int topStartPoint = topStart;

            if (relative)
            {
                leftStartPoint = getBorder(Side.Left) + leftStart;
                topStartPoint = getBorder(Side.Top) + topStart;
            }

            Console.SetCursorPosition(leftStartPoint, topStartPoint);
            Console.Write(text);
        }

        internal void fillScreen(string bg)
        {
            for (int i = getBorder(Side.Top); i <= getBorder(Side.Bottom); i++)
            {
                for (int k = getBorder(Side.Left); k <= getBorder(Side.Right); k++)
                {
                    Console.SetCursorPosition(k, i);
                    Console.Write(bg);
                }
            }
        }

        internal void fillScreen(string bg1, string bg2, bool hasOffset)
        {
            for (int i = getBorder(Side.Top); i <= getBorder(Side.Bottom); i++)
            {
                for (int k = getBorder(Side.Left); k < getBorder(Side.Right); k+=2)
                {
                    Console.SetCursorPosition(k, i);
                    if (hasOffset && i % 2 == 0)
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
    }
}
