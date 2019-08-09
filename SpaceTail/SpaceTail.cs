using SpaceTail.Game;
using System;

namespace SpaceTail
{
    class SpaceTail
    {
        static void Main(string[] args)
        {
            new SpaceTail(args);
        }

        public SpaceTail(string[] args)
        {
            var manager = new GameManager().SetArgs(args);
            manager.SetCurrentScene("Menu_Main");
            manager.StartGame();
        }
    }
}
