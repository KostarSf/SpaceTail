using System.Media;

namespace SpaceTail
{
    class AudioManager
    {
        public static void PlaySound(string fileName)
        {
            string file = $@"{Config.StaticWorkDir}{Config.AudioDir}{fileName}.wav";
            //string file = $@"{fileName}.wav";
            SoundPlayer sound = new SoundPlayer(file);
            sound.Play();
            sound.Dispose();
        }
    }
}
