﻿using System;

namespace SpaceTail
{
    public class Config
    {
        public static string Title = "SpaceTail";
        public static string SubTitle = "the story of one pony";
        public static string Version = "v0.1 ALPHA";
        public static string Author = "kostar";

        //80 20
        public static bool fullscreen = false;
        public static int WindowWidth = 80;
        public static int WindowHeight = 25;
        public static int AudioMaster = 10;
        public static int AudioSounds = 10;
        public static int AudioMusic = 10;

        public static string StaticWorkDir = @"C:\Users\kostar\source\repos\SpaceTail\SpaceTail\";
        public static string AudioDir = @"Resources\Audio\";

        public enum Option
        {
            WindowWidth,
            WindowHeight,
            AudioMaster,
            AudioSounds,
            AudioMusic,
            ResetScores,
            ResetOptions,
        }

        private static void setWindowSizes(int width, int height)
        {
            if (width < Console.LargestWindowWidth)
                WindowWidth = width;

            if (height < Console.LargestWindowHeight)
                WindowHeight = height;
        }

        public static void ResizeGameWindow(int newWidth, int newHeight)
        {
            setWindowSizes(newWidth, newHeight);

            resizeConsoleWindow(WindowWidth, WindowHeight);

            Interface.Initialize();
        }

        public static void ResizeGameWindowWidth(int amount)
        {
            ResizeGameWindow(WindowWidth + amount, WindowHeight);
        }

        public static void ResizeGameWindowHeight(int amount)
        {
            if ((WindowHeight + amount) >= 25)
                ResizeGameWindow(WindowWidth, WindowHeight + amount);
            else if ((WindowHeight + amount) < 25 && WindowHeight > 25)
                ResizeGameWindow(WindowWidth, 25);
        }

        public static void SetupGameWindow()
        {
            Console.Title = Title;
            if (fullscreen)
            {
                resizeConsoleWindow(Console.LargestWindowWidth, Console.LargestWindowHeight);
            }
            else
            {
                resizeConsoleWindow(WindowWidth, WindowHeight);
            }
            
        }

        private static void resizeConsoleWindow(int width, int height)
        {
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);
            Console.SetWindowSize(WindowWidth, WindowHeight);
        }
    }
}
