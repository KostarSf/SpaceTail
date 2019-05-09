using System;

namespace SpaceTail
{
    class Interface
    {
        private string menuTitle = Program.title;
        private string menuVersion = Program.version;

        struct Border
        {
            public int left;
            public int right;
            public int top;
            public int bottom;

            public void setBorders(int l, int r, int t, int b)
            {
                left = l;
                right = r;
                top = t;
                bottom = b;
            }

            public int[] getBorders()
            {
                int[] borders = { left, right, top, bottom };
                return borders;
            }
        }

        Border GameBorder = new Border();

        public Interface()
        {
            initialize();
            clearGameField();

        }

        public int[] getBorders()
        {
            return GameBorder.getBorders();
        }

        

        private void initialize()
        {
            Console.Clear();
            Console.CursorVisible = false;
            setGameBorders();
            drawBorders();
        }

        private void clearGameField()
        {
            for (int i = GameBorder.top; i <= GameBorder.bottom; i++)
            {
                for (int j = GameBorder.left; j <= GameBorder.right; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }
        }

        void setGameBorders()
        {
            GameBorder.setBorders(3, Console.WindowWidth - 2, 2, Console.WindowHeight - 3);
        }

        void drawBorders()
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
    }
}
