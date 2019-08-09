
using SpaceTail.Game.Scenes;
using System;
using System.Collections.Generic;

namespace SpaceTail.Game
{
    internal class GameManager
    {
        internal enum Menu
        {
            Main,

            NewGame,
            LoadGame,
            Continue,

            Pause,

            Options,
            About,
            Exit,
        }

        internal enum Options
        {
            VolumeMaster,
            VolumeSounds,
            VolumeMusic,

            Language,
        }

        private string[] gameArgs;

        private Dictionary<string, Scene> scenesList;

        private TextManager text;

        public GameManager()
        {
            text = new TextManager().SetLanguage(TextManager.Language.Russian);

            scenesList = new Dictionary<string, Scene>();

            AddLocalizedNames();
            AddMenuScenes();
        }

        private void AddLocalizedNames()
        {
            text.AddMenuText("Continue", "Continue", "Продолжить");
            text.AddMenuText("LoadGame", "LoadGame", "Загрузить игру");
            text.AddMenuText("NewGame", "NewGame", "Новая игра");
            text.AddMenuText("Options", "Options", "Опции");
            text.AddMenuText("About", "About", "Об игре");
            text.AddMenuText("Exit", "Exit", "Выход");

            text.AddMenuText("Back", "Back", "Назад");

            text.AddMenuText("GameSounds", "Sound options", "Громкость игры");
            text.AddMenuText("VolumeMaster", "Master", "Общая");
            text.AddMenuText("VolumeSounds", "Sounds", "Звуки");
            text.AddMenuText("VolumeMusic", "Music", "Музыка");
            text.AddMenuText("GameLanguage", "Game language", "Язык игры");

            text.AddMenuText("AboutTitle", $"SpaceTail {Variables.APP_VERSION}", $"SpaceTail {Variables.APP_VERSION}");
            text.AddMenuText("AboutText",
                            "This is the story of a pony that got lost in space. So help the brave Lucky Star to return home!", 
                            "Эта история повествует об одной поняшке, затерявшейся в космосе. Помогите же отважной Лаки Стар вернуться домой!");
        }

        private void AddMenuScenes()
        {
            AddScene(new MenuScene("Menu_Main", Menu.Main).AddMenuItems(
                    new MenuButton(text.GetMenuText("Continue")).LinkTo(Menu.Continue).SetActive(false),
                    new MenuButton(text.GetMenuText("LoadGame")).Disabled(),    // Если ни у одного пункта не задано
                                                                            // Значение Selected, то выделяется
                                                                            // Первый активный пункт сверху
                    new MenuButton(text.GetMenuText("NewGame")).LinkTo(Menu.NewGame),

                    new MenuBlankLine(),

                    new MenuButton(text.GetMenuText("Options")).LinkTo(Menu.Options),
                    new MenuButton(text.GetMenuText("About")).LinkTo(Menu.About),

                    new MenuBlankLine(),

                    new MenuButton(text.GetMenuText("Exit")).LinkTo(Menu.Exit)
                ));

            AddScene(new MenuScene("Menu_Options", Menu.Options).AddMenuItems(
                    new MenuTitle(text.GetMenuText("GameSounds")),      // По бокам надписи добавляются "- " и " -", 
                                                                        // только одна строка, если не первый элемент,
                                                                        // то добавляется MenuBlankLine сверху
                    new MenuOption(text.GetMenuText("VolumeMaster"), Options.VolumeMaster),     // Меняет выбранный параметр
                                                                                                // По бокам пункта появляются стрелки
                    new MenuOption(text.GetMenuText("VolumeSounds"), Options.VolumeSounds),
                    new MenuOption(text.GetMenuText("VolumeMusic"), Options.VolumeMusic),

                    new MenuTitle(text.GetMenuText("GameLanguage")),
                    new MenuOption(Options.Language),   

                    new MenuBackTo(text.GetMenuText("Back"), Menu.Main).Selected()    // добавляет MenuBlankLine выше себя
                                                            // выводит слово "назад" или "back"
                ));

            AddScene(new MenuScene("Menu_About", Menu.About).AddMenuItems(
                    new MenuTitle(text.GetMenuText("AboutTitle")),
                    new MenuText(text.GetMenuText("AboutText")),

                    new MenuBackTo(text.GetMenuText("Back"), Menu.Main).Selected()
                ));
        }

        private void AddScene(Scene scene)
        {
            throw new NotImplementedException();
        }

        internal GameManager SetArgs(string[] args)
        {
            gameArgs = args;
            return this;
        }

        internal void Start()
        {
            Screen.Init();
        }
    }
}