using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTail
{
    class Program
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public static string title = "SpaceTail";
        public static string version = "v1.0";
        //80 20
        private static int consoleWidth = 80;
        private static int consoleHeight = 25;
        public static string author = "kostar";

        public static string workDir = @"C:\Users\kostar\source\repos\SpaceTail\SpaceTail\";
        public static string audioDir = @"Resources\Audio\";

        static void Main(string[] args)
        {
            configureWindow(title, consoleWidth, consoleHeight);

            Game game = new Game();

            //pauseProgram();
            waitInputForQuit();
        }

        private static void configureWindow(string title, int width, int height)
        {
            Console.Title = title;

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }

        private static void pauseProgram()
        {
            Console.ReadKey(true);
        }

        private static void waitInputForQuit()
        {
            string text = "Программа завершена. Нажмите любую клавишу для выхода ";

            Console.Clear();
            Console.CursorVisible = true;

            Console.WriteLine();
            Console.WriteLine(text);
            Console.SetCursorPosition(text.Length, 1);
            Console.ReadKey(true);
        }

        public static void ShowText(string text)
        {
            string output = "";

            for (int i = 0; i < Console.BufferWidth; i++)
            {
                if (i <= text.Length)
                {
                    output += text[i];
                }
                else
                {
                    output += " ";
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(output);
        }
    }
}
