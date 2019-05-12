namespace SpaceTail
{
    class SpaceTail
    {
        static void Main(string[] args)
        {
            Events.Init();

            Interface.Init();

            Game game = new Game();
            game.Start();
        }
    }

    internal class Menu
    {
        //static string text = "";
        static string dirName = "DIR: /home/LuckyStar";

        internal static void OnKeyPress()
        {
            //text = $"Была нажата {Events.PressedKey.KeyChar}";
        }

        internal static void OnInterfaceDraw()
        {
            Interface.AddTextToFrame(dirName, 4, 3);
        }
    }
}
