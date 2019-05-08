using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        public static string author = "kostar";

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
            sceneManager.PlayMenuScene();
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
        MenuScene mainMenu;
        MenuScene gameMenu;
        MenuScene scoreMenu;
        MenuScene optionsMenu;
        MenuScene aboutMenu;
        MenuScene exitMenu;
        GameScene gameScene;

        ArrayList Scenes = new ArrayList();

        public SceneManager(Interface gameInterface)
        {
            startScene = new StartScene(gameInterface);

            mainMenu = new MenuScene(gameInterface);
            mainMenu.addItem(new MenuItem("Начать Игру", gameMenu, true));
            mainMenu.addItem(new MenuItem("Рекорды", scoreMenu));
            mainMenu.addItem(new MenuItem("Настройки", optionsMenu));
            mainMenu.addItem(new MenuItem("Об Игре", aboutMenu));
            mainMenu.addItem(new MenuItem("Выход", exitMenu));
        }

        public void LoadScene(string sceneName)
        {

        }

        public void PlayStartScene()
        {
            startScene.start();
        }

        internal void PlayMenuScene()
        {
            mainMenu.Show();
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

        internal void drawSprite(string[] sprite)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2;
            int topStartPoint = Console.WindowHeight / 2 - sprite.Length / 2;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                Console.SetCursorPosition(leftStartPoint, i);
                Console.Write(sprite[row++]);
            }
        }

        internal void drawSprite(string[] sprite, int topStart)
        {
            int leftStartPoint = Console.WindowWidth / 2 - sprite[0].Length / 2;
            int topStartPoint = getBorder(Side.Top) + topStart;

            int row = 0;
            for (int i = topStartPoint; i < topStartPoint + sprite.Length; i++)
            {
                Console.SetCursorPosition(leftStartPoint, i);
                Console.Write(sprite[row++]);
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

    class StartScene : Scene
    {
        string[] spriteAuthor = {
            ".         _                _                        .",
            ".        | | __ ___   ___ | |_  __ _  _ __          .",
            ".        | |/ // _ \\ / __|| __|/ _` || '__|         .",
            ".        |   <| (_) |\\__ \\| |_| (_| || |            .",
            ".        |_|\\_\\\\___/ |___/ \\__|\\__,_||_|            .",
            ".                                                   .",
        };

        string[] spriteTitle = {
            ".                                                   .",
            ".   __                                              .",
            ".  ((_  ._   _.  _  _   /\\\\      _  ._   _   _  ._  .",
            ".  __)) |_) (_| (_ (/_ /--\\\\ \\/ (/_ | | (_| (/_ |   .",
            ".       |                                _|         .",
            ".                                                   .",
        };

        public StartScene(Interface gameInterface)
        {
            setSceneBorders(gameInterface.getBorders());
        }

        public void start()
        {
            drawTransition(":", "#", 10);
            //fillScreen("*");
            drawSprite(spriteAuthor);

            Thread.Sleep(2000);

            fillScreen(".", " ", true);
            drawSprite(spriteTitle);

            Thread.Sleep(3000);

            drawTransition(" ", "#", 20);
        }

        private void drawTransition(string bg, string edge, int speed)
        {
            for (int i = getBorder(Side.Left); i <= getBorder(Side.Right); i++)
            {
                for (int j = getBorder(Side.Top); j <= getBorder(Side.Bottom); j++)
                {
                    Console.SetCursorPosition(i, j);

                    if (i == getBorder(Side.Right))
                        Console.Write($"{bg}");
                    else
                        Console.Write($"{bg}{edge}");
                }

                Thread.Sleep(speed);
            }
        }

        
    }

    class MenuScene : Scene
    {
        string blank16 = "                ";
        string[] spriteMenuTitle = {
            "   __                                              ",
            "  ((_  ._   _.  _  _   /\\\\      _  ._   _   _  ._  ",
            "  __)) |_) (_| (_ (/_ /--\\\\ \\/ (/_ | | (_| (/_ |   ",
            "       |                                _|         ",
        };

        List<MenuItem> menuItems = new List<MenuItem>();

        public MenuScene(Interface gameInterface)
        {
            setSceneBorders(gameInterface.getBorders());
        }

        public void addItem(MenuItem item)
        {
            menuItems.Add(item);
        }

        public List<MenuItem> GetMenuItems() => menuItems;

        public void Show()
        {
            fillScreen(" ");
            drawSprite(generateMenuList());
            drawSprite(spriteMenuTitle, 0);
            drawText(Program.version, getBorder(Side.Bottom), getBorder(Side.Right) - Program.version.Length, false);
            drawText($" by {Program.author}", getBorder(Side.Bottom), getBorder(Side.Left), false);

            updateMenuList();
        }

        private void updateMenuList()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                int selectedItem = -1;

                if (key == ConsoleKey.Enter
                    || key == ConsoleKey.Z)
                {
                    break;
                }

                foreach (MenuItem item in menuItems)
                {
                    if (item.IsSelected())
                    {
                        selectedItem = menuItems.IndexOf(item);
                    }
                }

                if (key == ConsoleKey.DownArrow)
                {
                    menuItems[selectedItem].SetSelected(false);
                    selectedItem++;
                    if (selectedItem == menuItems.Count)
                    {
                        selectedItem = 0;
                    }
                    menuItems[selectedItem].SetSelected(true);
                }
                else
                if (key == ConsoleKey.UpArrow)
                {
                    menuItems[selectedItem].SetSelected(false);
                    if (selectedItem == 0)
                    {
                        selectedItem = menuItems.Count - 1;
                    }
                    else
                    {
                        selectedItem--;
                    }
                    menuItems[selectedItem].SetSelected(true);
                }

                drawSprite(generateMenuList());
            }
        }

        private string[] generateMenuList()
        {
            int sideOffset = 8;
            string sideChar = "*";

            List<string> menuList = new List<string>();

            int maxMenuItemTextLength = 1;

            foreach (MenuItem item in menuItems)
            {
                if (item.GetMenuItemText().Length > maxMenuItemTextLength)
                {
                    maxMenuItemTextLength = item.GetMenuItemText().Length;
                }
            }

            foreach (MenuItem item in menuItems)
            {
                string menuItemText = item.GetMenuItemText();
                string menuItemOffset = "";
                string menuItemSideChar = " ";

                string menuItemFinal;

                if (menuItemText.Length % 2 == 0)
                {
                    menuItemText += " ";
                }

                for (int i = 0; i < sideOffset + (maxMenuItemTextLength - menuItemText.Length) / 2; i++)
                {
                    menuItemOffset += " ";
                }

                if (item.IsSelected())
                {
                    menuItemSideChar = sideChar;
                }

                menuItemFinal = menuItemSideChar + menuItemOffset + menuItemText + menuItemOffset + menuItemSideChar;

                menuList.Add(menuItemFinal);
            }

            return menuList.ToArray();
        }
    }

    class MenuItem
    {
        string itemText;
        Scene itemLink;
        bool isSelected;

        public MenuItem(string itemText, Scene itemLink)
        {
            this.itemText = itemText;
            this.itemLink = itemLink;
        }

        public MenuItem(string itemText, Scene itemLink, bool selected)
        {
            this.itemText = itemText;
            this.itemLink = itemLink;
            isSelected = selected;
        }

        public string GetMenuItemText() => itemText;

        public bool IsSelected()
        {
            return isSelected;
        }

        public void SetSelected(bool state)
        {
            isSelected = state;
        }
    }

    class GameScene : Scene
    {

    }
}
