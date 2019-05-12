
using System;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTail
{
    internal class Interface
    {
        static int windowWidth;
        static int windowHeight;

        static char[,] previousFrame = new char[Console.LargestWindowHeight, Console.LargestWindowWidth];
        static char[,] currentFrame = new char[Console.LargestWindowHeight, Console.LargestWindowWidth];

        public delegate void InterfaceDrawEvent();

        public static event InterfaceDrawEvent onInterfaceDraw;

        internal async static void Init()
        {
            setupConsoleWindow();
            

            DateTime nt = DateTime.Now;
            int tic = 200;

            await Task.Run(() =>
            {
                
                while (true)
                {
                    if ((DateTime.Now - nt).TotalMilliseconds > tic)
                    {
                        drawInterface();

                        nt = DateTime.Now;
                    }
                }
            });
        }

        internal static void AddTextToFrame(string text, int left, int top)
        {
            for (int i = 0; i < text.Length; i++)
            {
                currentFrame[top, left + i] = text[i];
            }
        }

        private static void setupConsoleWindow()
        {
            windowWidth = Console.WindowWidth;
            windowHeight = Console.WindowHeight-1;
            ResizeConsoleWindow(windowWidth, windowHeight, true);
        }

        private static void setWindowSizes(int width, int height)
        {
            windowWidth = width;
            windowHeight = height;
            ResizeConsoleWindow(windowWidth, windowHeight, true);
        }

        public static void ResizeConsoleWindow(int consoleWidth, int consoleHeight, bool clearBuffer)
        {
            if (clearBuffer)
            {
                Console.Clear();
            }

            try
            {
                Console.SetWindowSize(consoleWidth, consoleHeight + 1);
                Console.SetBufferSize(consoleWidth, consoleHeight + 1);
                Console.SetWindowSize(consoleWidth, consoleHeight + 1);
            }
            catch (System.IO.IOException)
            {
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;
                Console.SetBufferSize(windowWidth, windowHeight);
                Console.SetWindowSize(windowWidth, windowHeight);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;
                Console.SetBufferSize(windowWidth, windowHeight);
                Console.SetWindowSize(windowWidth, windowHeight);
            }
        }

        private static void drawInterface()
        {
            windowResizeCheck();
            createFrame();
            drawFrame();
        }

        private static void windowResizeCheck()
        {
            if (Math.Abs(Console.WindowWidth - windowWidth) > 1
                || Math.Abs(Console.WindowHeight - windowHeight) > 1)
            {
                setWindowSizes(Console.WindowWidth, Console.WindowHeight);
                drawBorders();
            }
            Console.CursorVisible = false;
        }

        private static void createFrame()
        {
            //Очистка и заполнение фрейма пробелами
            for (int row = 0; row < windowHeight; row++)
            {
                for (int col = 0; col < windowWidth; col++)
                {
                    currentFrame[row, col] = ' ';
                }
            }

            drawBorders();
            onInterfaceDraw();
        }

        private static void drawBorders()
        {
            for (int row = 1; row < windowHeight; row++)
            {
                for (int col = 2; col < windowWidth-2; col++)
                {
                    if (row == 1 || row == windowHeight - 1)
                    {
                        currentFrame[row, col] = Resources.Sprites.ConsoleBorder.Horizontal;
                    }

                    if (col == 2 || col == windowWidth - 3)
                    {
                        currentFrame[row, col] = Resources.Sprites.ConsoleBorder.Vertical;
                    }

                    if (row == 1 && col == 2)
                        currentFrame[row, col] = Resources.Sprites.ConsoleBorder.TopLeft;
                    if (row == 1 && col == windowWidth - 3)
                        currentFrame[row, col] = Resources.Sprites.ConsoleBorder.TopRight;
                    if (row == windowHeight - 1 && col == 2)
                        currentFrame[row, col] = Resources.Sprites.ConsoleBorder.BottomLeft;
                    if (row == windowHeight - 1 && col == windowWidth - 3)
                        currentFrame[row, col] = Resources.Sprites.ConsoleBorder.BottomRight;
                }
            }
        }

        private static void drawFrame()
        {
            StringBuilder line = new StringBuilder();

            for (int row = 0; row < windowHeight; row++)
            {
                line.Clear();
                for (int col = 0; col < windowWidth; col++)
                {
                    line.Append(currentFrame[row, col]);
                }
                
                Console.SetCursorPosition(0, row+1);
                Console.Write(line.ToString());
            }
            previousFrame = currentFrame;
        }
    }
}