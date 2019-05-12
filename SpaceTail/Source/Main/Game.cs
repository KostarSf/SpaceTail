namespace SpaceTail
{
    class Game
    {
        //стандартный размер консоли: 120 30
        //private bool paused;
        //private bool endGame;

        AudioManager audioManager;
        SceneManager sceneManager;

        public Game()
        {
            audioManager = new AudioManager();
            audioManager.createSoundList(Resources.GetSoundsList());
            audioManager.createMusicList(Resources.GetMusicList());

            sceneManager = new SceneManager();
            //sceneManager.LoadScene("StartScene");
            //sceneManager.PlayStartScene(); 
            sceneManager.PlayMenuScene();

            //test change
            //test 2
        }
    }
}
