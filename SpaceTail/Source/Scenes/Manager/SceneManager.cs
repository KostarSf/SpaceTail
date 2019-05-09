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

        public SceneManager(Interface gameInterface)
        {
            startScene = new StartScene(gameInterface);

            mainMenu = new MenuScene(gameInterface);
            gameMenu = new MenuScene(gameInterface);
            loadMenu = new MenuScene(gameInterface);
            scoreMenu = new MenuScene(gameInterface);
            optionsMenu = new MenuScene(gameInterface);
            aboutMenu = new MenuScene(gameInterface);
            exitMenu = new MenuScene(gameInterface);

            MenuItem menuItem;

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

            gameMenu.addMenuItem(new MenuItem("Назад", mainMenu));
            gameMenu.setMenuItemAttributes(true);

            loadMenu.addMenuItem(new MenuItem("Назад", mainMenu));
            loadMenu.setMenuItemAttributes(true);

            scoreMenu.addMenuItem(new MenuItem("Назад", mainMenu));
            scoreMenu.setMenuItemAttributes(true);

            optionsMenu.addMenuItem(new MenuItem("Назад", mainMenu));
            optionsMenu.setMenuItemAttributes(true);

            aboutMenu.addTextItem($"{Program.title} {Program.version}");
            aboutMenu.addVoidMenuItem();
            aboutMenu.addTextItem("Эта история повествует об одной");
            aboutMenu.addTextItem("поняшке, затерявшейся в космосе.");
            aboutMenu.addTextItem("Помогите же отважной Лаки Стар");
            aboutMenu.addTextItem("вернуться домой!");
            aboutMenu.addVoidMenuItem();
            aboutMenu.addMenuItem(new MenuItem("Назад", mainMenu));
            aboutMenu.setMenuItemAttributes(true);

            exitMenu.addTextItem("Выйти?");
            exitMenu.addVoidMenuItem();
            exitMenu.addMenuItem(new MenuItem("Да", null));
            exitMenu.setMenuItemAttributes(true, true);
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
