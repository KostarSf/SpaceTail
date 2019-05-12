using System.Collections;
using System.Diagnostics;

namespace SpaceTail
{
    class SceneManager
    {
        StartScene startScene;
        MenuScene mainMenu;
        MenuScene gameMenu;
        MenuScene loadMenu;
        MenuScene scoreMenu;
        MenuScene optionsMenu;
        MenuScene aboutMenu;
        MenuScene exitMenu;
        //GameScene gameScene;

        ArrayList Scenes = new ArrayList();

        public SceneManager()
        {
            startScene = new StartScene();

            mainMenu = new MenuScene();
            gameMenu = new MenuScene();
            loadMenu = new MenuScene();
            scoreMenu = new MenuScene();
            optionsMenu = new MenuScene();
            aboutMenu = new MenuScene();
            exitMenu = new MenuScene();

            mainMenu.addMenuItem(new MenuItem("Начать Игру", gameMenu));
            mainMenu.setMenuItemAttributes(true, true);
            mainMenu.addMenuItem(new MenuItem("Загрузить", loadMenu));
            mainMenu.setMenuItemAttributes(false, false);
            mainMenu.addVoidMenuItem();
            mainMenu.addMenuItem(new MenuItem("Рекорды", scoreMenu));
            mainMenu.addMenuItem(new MenuItem("Настройки", optionsMenu));
            mainMenu.addMenuItem(new MenuItem("Об Игре", aboutMenu));
            mainMenu.addVoidMenuItem();
            mainMenu.addMenuItem(new MenuItem("Выход", exitMenu));

            gameMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            loadMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            scoreMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            optionsMenu.addTextItem("Размеры Окна: ");
            optionsMenu.addOptionItem("Ширина: ", Config.Option.WindowWidth);
            optionsMenu.addOptionItem("Высота: ", Config.Option.WindowHeight);
            optionsMenu.addVoidMenuItem();
            optionsMenu.addTextItem("Громкость Игры:");
            optionsMenu.addOptionItem("Общая: ", Config.Option.AudioMaster);
            optionsMenu.addOptionItem("Звуки: ", Config.Option.AudioSounds);
            optionsMenu.addOptionItem("Музыка: ", Config.Option.AudioMusic);
            optionsMenu.addVoidMenuItem();
            optionsMenu.addOptionItem("Сбросить Рекорды", Config.Option.ResetScores);
            optionsMenu.addOptionItem("Сбросить Настройки", Config.Option.ResetOptions);
            optionsMenu.addVoidMenuItem();
            optionsMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            aboutMenu.addTextItem($"{Config.Title} {Config.Version}");
            aboutMenu.addVoidMenuItem();
            aboutMenu.addTextItem("Эта история повествует об одной");
            aboutMenu.addTextItem("поняшке, затерявшейся в космосе.");
            aboutMenu.addTextItem("Помогите же отважной Лаки Стар");
            aboutMenu.addTextItem("вернуться домой!");
            aboutMenu.addVoidMenuItem();
            aboutMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            exitMenu.addTextItem("Выйти?");
            exitMenu.addVoidMenuItem();
            exitMenu.addMenuItem(new MenuItem("Да", null, true));
            exitMenu.addMenuItem(new MenuItem("Нет", mainMenu));
        }

        public void LoadScene(string sceneName)
        {

        }

        public void PlayStartScene()
        {
            startScene.start();
        }

        internal void PlayMenuScene()
        {
            Debug.WriteLine("Musics: " + AudioManager.MusicList.Count);
            Debug.WriteLine("Sounds: " + AudioManager.SoundList.Count);
            AudioManager.PlayMusic("WhisperOfStars");
            mainMenu.Show();
            
        }
    }
}
