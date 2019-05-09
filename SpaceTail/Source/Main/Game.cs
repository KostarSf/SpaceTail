namespace SpaceTail
{
    class Game
    {
        //стандартный размер консоли: 120 30
        private bool paused;
        private bool endGame;

        SceneManager sceneManager;

        public Game()
        {
            Interface gameInterface = new Interface();
            sceneManager = new SceneManager(gameInterface);
            //sceneManager.LoadScene("StartScene");
            sceneManager.PlayStartScene(); 
            sceneManager.PlayMenuScene();
        }
    }
}
