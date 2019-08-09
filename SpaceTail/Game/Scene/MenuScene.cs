using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail.Game.Scenes
{
    class MenuScene : Scene
    {
        public MenuScene(string menuName, GameManager.Menu menu) : base(menuName)
        {
        }

        internal MenuScene AddMenuItems(params MenuItem[] items)
        {

            return this;
        }
    }
}
