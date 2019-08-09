﻿using SpaceTail.Game;
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
            var manager = new Game.GameManager().SetArgs(args);
            manager.Start();
        }
    }
}
