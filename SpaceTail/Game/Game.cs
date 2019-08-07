using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceTail
{
    class Game
    {
        static Task inputCycle;
        static int inputCount = 0;
        static int exceptionCount = 0;
        static int lastWindowWidth = 0;
        static int lastWindowHeight = 0;
        static long elapsedTime;

        static string fpsOffset = "";
        public static bool GameIsRunning;
        static int[] consoleSizes = { 0, 0 };

        static int fps = 20;
        static int targetCycleTime = 1000 / fps;

        static List<int> fpsSmooth = new List<int>();

        static int cycleCounter = 0;
        static List<TextSprite> SpriteList = new List<TextSprite>();

        static List<string> testArray = new List<string>();

        public static void StartGame()
        {
            testArray.Add("SpaceTail");
            testArray.Add("The story of one pony");

            Game.SetFPS(20);
            fpsSmooth = Input.PutInts(0, 40);

            GameIsRunning = true;
            inputCount = 0;
            exceptionCount = 0;
            Screen.Clear();

            var frame = new Frame();

            Stopwatch cycleTimer = new Stopwatch();
            int currentCycleTime;

            Stopwatch fpsTimer = new Stopwatch();

            TestPlayer player = new TestPlayer(5, 10);

            consoleSizes[0] = Console.WindowWidth;
            consoleSizes[1] = Console.WindowHeight;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetBufferSize(consoleSizes[0], consoleSizes[1]);
            }

            inputCycle = new Task(UpdateInput);
            inputCycle.Start();

            while (GameIsRunning)
            {
                fpsTimer.Restart();
                cycleTimer.Restart();

                UpdateLogic();
                DrawFrame(frame);

                cycleTimer.Stop();
                currentCycleTime = (int)cycleTimer.ElapsedMilliseconds;

                if (currentCycleTime < targetCycleTime)
                {
                    Thread.Sleep(targetCycleTime - currentCycleTime);
                }

                cycleCounter++;

                elapsedTime = Input.Average(fpsSmooth);

                switch (elapsedTime.ToString().Length)
                {
                    case 1:
                        fpsOffset = "  ";
                        break;
                    case 2:
                        fpsOffset = " ";
                        break;
                    case 3:
                        fpsOffset = "";
                        break;
                }

                fpsSmooth.RemoveAt(0);
                fpsSmooth.Add((int)(1000 / fpsTimer.ElapsedMilliseconds));
            }

            inputCycle.Wait();
            inputCycle.Dispose();
            SpaceTail.DrawConsoleMenu(SpaceTail.Menu.DevMenu);
        }

        private static void UpdateLogic()
        {
            
        }

        private static void UpdateInput()
        {
            ConsoleKeyInfo pressedKey;

            while (GameIsRunning)
            {
                pressedKey = Console.ReadKey(true);
                inputCount++;

                if (pressedKey.Key == ConsoleKey.Q)
                {
                    GameIsRunning = false;
                }

                switch (pressedKey.Key)
                {
                    case ConsoleKey.Q:
                        GameIsRunning = false;
                        break;

                    case ConsoleKey.UpArrow:
                        if (TestPlayer.Y > 1)
                            TestPlayer.Y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (TestPlayer.Y < Console.BufferHeight - 4)
                            TestPlayer.Y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (TestPlayer.X > 1)
                            TestPlayer.X--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (TestPlayer.X < Console.BufferWidth - 6)
                            TestPlayer.X++;
                        break;

                    case ConsoleKey.D1:
                        Game.SetFPS(10);
                        break;
                    case ConsoleKey.D2:
                        Game.SetFPS(20);
                        break;
                    case ConsoleKey.D3:
                        Game.SetFPS(30);
                        break;
                    case ConsoleKey.D6:
                        Game.SetFPS(60);
                        break;
                    case ConsoleKey.D9:
                        Game.SetFPS(100);
                        break;
                    case ConsoleKey.D0:
                        Game.SetFPS(1000);
                        break;
                }
            }
        }

        private static void SetFPS(int value)
        {
            Game.fps = value;
            Game.targetCycleTime = 1000 / fps;
        }

        private static void DrawFrame(Frame frame)
        {
            Screen.HideCursor();
            frame.Clear();

            // Временное пристанище выводимых спрайтов, 
            // вместо них тут будет метод добавления массива спрайтов из цикла с логикой

            Game.SpriteList.Clear();
            
            Game.SpriteList.Add(new TextSprite(Game.GetFPS(frame), 2, 1));
            Game.SpriteList.Add(new TextSprite("'q' to quit", 2, 2));
            Game.SpriteList.Add(new TextSprite("1, 2, 3, 6, 9, 0 - time control", 2, 3));
            Game.SpriteList.Add(new TextSprite("arrows - the 'X' control", 2, 4));

            Game.SpriteList.Add(new TextSprite("by KostarSf", 2, 1).SetScreenAlign(Screen.ScreenAlign.BottomLeft));
            Game.SpriteList.Add(new TextSprite(SpaceTail.appVersion, 2, 1).SetScreenAlign(Screen.ScreenAlign.BottomRight));

            Game.SpriteList.Add(new TextSprite(testArray, 0, 0).SetAlign(Screen.TextAlign.Center).SetScreenAlign(Screen.ScreenAlign.MiddleCenter));

            Game.SpriteList.Add(new TextSprite(TestPlayer.Symbol.ToString(), TestPlayer.X, TestPlayer.Y).SetColors(ConsoleColor.Red, Input.GetDefaultBackgroundColor()));

            //Слежение за обновлением размера консоли

            if (Console.WindowWidth != lastWindowWidth)
            {
                lastWindowWidth = Console.WindowWidth;
                Console.Clear(); //Убрать, когда появится буфер кадров
                frame.FitSizesToWindow();
            }

            if (Console.WindowHeight != lastWindowHeight)
            {
                lastWindowHeight = Console.WindowHeight;
                frame.FitSizesToWindow();
            }

            if (Console.BufferHeight > Console.WindowHeight)
            {
                Console.Clear();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.BufferHeight = Console.WindowHeight;
                    Console.WindowWidth = Console.BufferWidth;
                }
                frame.FitSizesToWindow();
            }

            //Вывод графики

            frame.SetSpriteList(Game.SpriteList);
            frame.Draw();
        }

        private static string GetFPS(Frame frame)
        {
            var fps = new StringBuilder();

            switch (cycleCounter)
            {
                case 1:
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  -   ");
                    break;
                case 2:
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  \   ");
                    break;
                case 3:
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  |   ");
                    break;
                case 4:
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  /   ");
                    cycleCounter = 0;
                    break;
            }

            return fps.ToString();
        }

        internal static void GetSecretEnding(Exception e)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WindowHeight = 20;
                Console.SetCursorPosition(0, 0);
            }

            Screen.ShowCursor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\n The secret exit way has been applied! ^_^\n");
            Console.ResetColor();

            throw new Exception(e.Message);
        }
    }
}
