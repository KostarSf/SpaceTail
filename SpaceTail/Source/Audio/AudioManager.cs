using SpaceTail.Source.Audio;
using System.Collections.Generic;

namespace SpaceTail
{
    class AudioManager
    {
        public static List<Music> MusicList = new List<Music>();
        public static List<Sound> SoundList = new List<Sound>();

        internal void createMusicList(List<Music> musicList)
        {
            MusicList = musicList;
        }

        internal void createSoundList(List<Sound> soundList)
        {
            SoundList = soundList;
        }

        public static void PlayMusic(string musicName)
        {
            foreach (Music music in MusicList)
            {
                if (music.Name == musicName)
                    music.Loop();
            }
        }

        public static void PlayMusicOnce(string musicName)
        {
            foreach (Music music in MusicList)
            {
                if (music.Name == musicName)
                    music.Play();
            }
        }

        public static void StopMusic(string musicName)
        {
            foreach (Music music in MusicList)
            {
                if (music.Name == musicName)
                    music.Stop();
            }
        }

        public static void StopAllMusic()
        {
            foreach (Music music in MusicList)
            {
                music.Stop();
            }
        }

        public static void StopSound(string soundName)
        {
            foreach (Sound sound in SoundList)
            {
                if (sound.Name == soundName)
                {
                    sound.Stop();
                }

            }
        }

        public static void StopAllSounds()
        {
            foreach (Sound sound in SoundList)
            {

                sound.Stop();


            }
        }

        public static void PlaySound(string soundName)
        {
            foreach (Sound sound in SoundList)
            {
                if (sound.Name == soundName)
                {
                    sound.Play();
                }

            }
        }

        public static void LoopSound(string soundName)
        {
            foreach (Sound sound in SoundList)
            {
                if (sound.Name == soundName)
                {
                    sound.Loop();
                }

            }
        }

        //public static void PlaySound(string fileName)
        //{
        //    string file = $@"{Config.StaticWorkDir}{Config.AudioDir}{fileName}.wav";
        //    if (!File.Exists(file))
        //    {
        //        file = $@"{fileName}.wav";

        //        if (!File.Exists(file))
        //        {
        //            Interface.ShowAlert($"Can't find '{fileName}' sound!");
        //            return;
        //        }
        //    }
        //    Interface.ClearAlert();

        //    var thread = new Thread(playSound);
        //    thread.Start();

        //    void playSound()
        //    {
        //        using (var audioFile = new AudioFileReader(file))
        //        using (var outputDevice = new WaveOutEvent())
        //        {
        //            outputDevice.Init(audioFile);
        //            outputDevice.Play();
        //            Thread.Sleep(300);
        //            outputDevice.Dispose();
        //            audioFile.Dispose();
        //        }
        //    }
        //}


    }
}
