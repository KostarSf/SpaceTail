using System;

namespace SpaceTail.Game
{
    internal class MenuBackTo : MenuButton
    {
        public MenuBackTo(string buttonText, GameManager.Menu linkedMenu) : base(buttonText)
        {
            LinkTo(linkedMenu);
        }
    }
}
