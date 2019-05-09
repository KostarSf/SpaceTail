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

        public StartScene()
        {
            
        }

        public void start()
        {
            Interface.DrawTransition(":", "#", 10);

            Interface.DrawCenteredSprite(spriteAuthor);
            Thread.Sleep(2000);

            Interface.FillPatternScreen(".", " ", true);
            Interface.DrawCenteredSprite(spriteTitle);
            Thread.Sleep(3000);

            Interface.DrawTransition(" ", "#", 20);
        }
    }
}
