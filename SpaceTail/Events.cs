using System;
using System.Threading.Tasks;

namespace SpaceTail
{
    internal class Events
    {
        public static ConsoleKeyInfo PressedKey;

        internal async static void Init()
        {
            KeyListener keyListener = new KeyListener();
            keyListener.onKeyPress += Menu.OnKeyPress;

            Interface.onInterfaceDraw += Menu.OnInterfaceDraw;

            

            await Task.Run(() => keyListener.startListen());
        }

        internal class KeyListener
        {
            public delegate void KeyListenerEvent();

            public event KeyListenerEvent onKeyPress;

            internal void startListen()
            {
                while (true)
                {
                    Events.PressedKey = Console.ReadKey(true);
                    onKeyPress();
                }
            }
        }


    }
}