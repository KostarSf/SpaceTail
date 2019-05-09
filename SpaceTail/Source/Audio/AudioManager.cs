using System.IO;
using System.Media;

namespace SpaceTail
{
    static class AudioManager
    {
        static SoundPlayer soundPlayer = new SoundPlayer();

        public static void PlaySound(string fileName)
        {
            string file = $@"{Config.StaticWorkDir}{Config.AudioDir}{fileName}.wav";
            if (!File.Exists(file))
            {
                file = $@"{fileName}.wav";

                if (!File.Exists(file))
                {
                    Interface.ShowAlert($"Can't find '{fileName}' sound!");
                    return;
                }
            }
            Interface.ClearAlert();

            soundPlayer.SoundLocation = file;
            soundPlayer.Play();
            soundPlayer.Dispose();
        }
    }
}
