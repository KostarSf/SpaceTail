using System;

namespace SpaceTail
{
    public class Config
    {
        public static string Title = "SpaceTail";
        public static string SubTitle = "the story of one pony";
        public static string Version = "v0.1 ALPHA";
        public static string Author = "kostar";

        //80 20
        public static int WindowWidth = 80;
        public static int WindowHeight = 25;

        public static string StaticWorkDir = @"C:\Users\kostar\source\repos\SpaceTail\SpaceTail\";
        public static string AudioDir = @"Resources\Audio\";

        private static void setWindowSizes(int width, int height)
        {
            WindowWidth = width;
            WindowHeight = height;
        }

        public static void ResizeGameWindow(int newWidth, int newHeight)
        {
            setWindowSizes(newWidth, newHeight);

            Console.SetBufferSize(WindowWidth, WindowHeight);
            Console.SetWindowSize(WindowWidth, WindowHeight);
        }

        public static void SetupGameWindow()
        {
            Console.Title = Title;
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);
        }
    }
}
