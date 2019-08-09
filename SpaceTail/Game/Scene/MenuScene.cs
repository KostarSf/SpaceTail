using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail.Game.Scenes
{
    class MenuScene : Scene
    {
        private List<MenuItem> itemsList;

        public MenuScene(string menuName, GameManager.Menu menu) : base(menuName)
        {
            itemsList = new List<MenuItem>();
        }

        internal MenuScene AddMenuItems(params MenuItem[] items)
        {
            foreach (var item in items)
            {
                itemsList.Add(item);
            }

            return this;
        }

        bool isKeyPressed;
        bool isShowText;
        ConsoleKeyInfo pressedKey;
        long startTick;
        string showedText;
        public override void OnLogicUpdate(long ticksPassed)
        {
            Console.Clear();

            if (isKeyPressed)
            {
                isKeyPressed = false;
                isShowText = true;
                startTick = ticksPassed;
            }

            if (isShowText)
            {
                if (ticksPassed - startTick < 20)
                {
                    showedText = $"{Input.GetEmptyLine((int)((ticksPassed - startTick)/4))} Hooked '{pressedKey.KeyChar}' in the '{Name}' scene";

                    switch (ticksPassed - startTick)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                    }
                }
                else
                {
                    isShowText = false;
                }
            }
        }

        public override void OnKeyPressed(ConsoleKeyInfo key)
        {
            isKeyPressed = true;
            pressedKey = key;

            //Console.WriteLine($"\nHooked '{key.KeyChar}' in the '{Name}' scene");
        }

        public override void OnFrameDraw()
        {
            //Console.Write($". ");
            if (isShowText)
            {
                Console.WriteLine(showedText);
            }
            else
            {
                Console.Clear();
            }
        }
    }
}
