using System.Collections;

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
        GameScene gameScene;

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
            mainMenu.setMenuItemAttributes(true, true, true);
            mainMenu.addMenuItem(new MenuItem("Загрузить", loadMenu));
            mainMenu.setMenuItemAttributes(false, false);
            mainMenu.addVoidMenuItem();
            mainMenu.addMenuItem(new MenuItem("Рекорды", scoreMenu));
            mainMenu.setMenuItemAttributes(false, true, true);
            mainMenu.addMenuItem(new MenuItem("Настройки", optionsMenu));
            mainMenu.setMenuItemAttributes(false, true, true);
            mainMenu.addMenuItem(new MenuItem("Об Игре", aboutMenu));
            mainMenu.addVoidMenuItem();
            mainMenu.addMenuItem(new MenuItem("Выход", exitMenu));

            gameMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            loadMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

            scoreMenu.addMenuItem(new MenuItem("Назад", mainMenu, true));

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
            mainMenu.Show();
        }
    }
}
