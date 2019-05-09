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
            sceneManager = new SceneManager();
            //sceneManager.LoadScene("StartScene");
            sceneManager.PlayStartScene(); 
            sceneManager.PlayMenuScene();
        }
    }
}
