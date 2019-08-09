using System;
using System.Collections.Generic;

namespace SpaceTail.Game
{
    internal class TextManager
    {
        public enum Language
        {
            English,
            Russian,
        }

        private static Language _currentLanguage = Language.Russian;
        private static Dictionary<string, Dictionary<Language, string>> _menu;

        internal static Language CurrentLanguage { get => _currentLanguage; }
        public static Dictionary<string, Dictionary<Language, string>> Menu { get => _menu; }

        

        public TextManager()
        {
            _menu = new Dictionary<string, Dictionary<Language, string>>();
        }

        internal void AddMenuText(string internalName, params string[] localizedNames)
        {
            var textNames = new Dictionary<Language, string>();

            for (int i = 0; i < localizedNames.Length; i++)
            {
                textNames.Add((Language)i, localizedNames[i]);
            }

            _menu.Add(internalName, textNames);
        }

        internal string GetMenuText(string text)
        {
            if (Menu[text][CurrentLanguage] != null)
            {
                return Menu[text][CurrentLanguage];
            }
            else
            {
                return $"[ {text} ]";
            }
        }

        internal TextManager SetLanguage(Language language)
        {
            _currentLanguage = language;

            return this;
        }
    }
}