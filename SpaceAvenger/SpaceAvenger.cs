using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAvenger
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

        public static string title = "Space Avenger";
        public static string version = "v1.0";
        private static int consoleWidth = 80;
        private static int consoleHeight = 20;

        static void Main(string[] args)
        {
            configureWindow(title, consoleWidth, consoleHeight);

            Game game = new Game();

            pauseProgram();
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
            string text = "Программа завершена. Нажмите любую клавишу для выхода";

            Console.Clear();
            Console.CursorVisible = true;

            Console.WriteLine();
            Console.WriteLine(text);
            Console.SetCursorPosition(text.Length, 1);
            Console.ReadKey(true);
        }
    }

    class Game
    {
        //стандартный размер консоли: 120 30
        private bool paused;
        private bool endGame;

        SceneManager sceneManager;

        public Game()
        {
            Interface gameInterface = new Interface();
            sceneManager = new SceneManager(gameInterface);
            //sceneManager.LoadScene("StartScene");
            sceneManager.PlayStartScene();
        }
    }

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

    class SceneManager
    {
        StartScene startScene;
        MenuScene menuScene;
        GameScene gameScene;

        ArrayList Scenes = new ArrayList();

        public SceneManager(Interface gameInterface)
        {
            startScene = new StartScene(gameInterface);
        }

        public void LoadScene(string sceneName)
        {

        }

        public void PlayStartScene()
        {
            startScene.start();
        }
    }

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
    }

    class StartScene : Scene
    {
        

        public StartScene(Interface gameInterface)
        {
            setSceneBorders(gameInterface.getBorders());
        }

        public void start()
        {
            FillScreen();
        }

        private void FillScreen()
        {
            for (int i = getBorder(Side.Top); i <= getBorder(Side.Bottom); i++)
            {
                for (int k = getBorder(Side.Left); k <= getBorder(Side.Right); k++)
                {
                    Console.SetCursorPosition(k, i);
                    Console.Write("*");
                }
            }
        }
    }

    class MenuScene : Scene
    {

    }

    class GameScene : Scene
    {

    }
}
