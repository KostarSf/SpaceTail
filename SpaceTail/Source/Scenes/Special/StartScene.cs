using System;
using System.Threading;

namespace SpaceTail
{
    class StartScene : Scene
    {
        string[] spriteAuthor = {
            ".                                                     .",
            ".                                                     .",
            ".                                                     .",
            ".          __                __                       .",
            ".         |  |--.-----.-----|  |_.---.-.----.         .",
            ".         |    <|  _  |__ --|   _|  _  |   _|         .",
            ".         |__|__|_____|_____|____|___._|__|           .",
            ".                                                     .",
            ".                                                     .",
            ".                                                     .",
        };

        string[] spriteTitle = {
            ".                                                     .",
            ".         _____                 _____     _ _         .",
            ".        |   __|___ ___ ___ ___|_   _|___|_| |        .",
            ".        |__   | . | .'|  _| -_| | | | .'| | |        .",
            ".        |_____|  _|__,|___|___| |_| |__,|_|_|        .",
            ".              |_|           _                        .",
            ".       _// _    __/  _     (_      _                 .",
            ".       //)(-  _) /()/ (/ ()/  ()/)(-  /)()/)(/       .",
            ".                      /              /      /        .",
            ".                                                     .",
        };

        public StartScene(Interface gameInterface)
        {
            setSceneBorders(gameInterface.getBorders());
        }

        public void start()
        {
            drawTransition(":", "#", 10);
            //fillScreen("*");
            drawCenteredSprite(spriteAuthor);

            Thread.Sleep(2000);

            fillScreen(".", " ", true);
            drawCenteredSprite(spriteTitle);

            Thread.Sleep(3000);

            drawTransition(" ", "#", 20);
        }

        private void drawTransition(string bg, string edge, int speed)
        {
            for (int i = getBorder(Side.Left); i <= getBorder(Side.Right); i++)
            {
                for (int j = getBorder(Side.Top); j <= getBorder(Side.Bottom); j++)
                {
                    Console.SetCursorPosition(i, j);

                    if (i == getBorder(Side.Right))
                        Console.Write($"{bg}");
                    else
                        Console.Write($"{bg}{edge}");
                }

                Thread.Sleep(speed);
            }
        }

        
    }
}
