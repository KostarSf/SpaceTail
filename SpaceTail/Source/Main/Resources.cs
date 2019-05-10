using SpaceTail.Source.Audio;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceTail
{
    internal class Resources
    {
        static List<Sound> soundList = new List<Sound>();
        static List<Music> musicList = new List<Music>();

        internal static List<Music> GetMusicList()
        {
            string[] musicFiles = Directory.GetFiles(Config.StaticWorkDir + Config.MusicDir, "*.ogg");

            musicList.Clear();

            foreach (string file in musicFiles)
            {
                var musicFile = new FileInfo(file);
                musicList.Add(new Music(musicFile.Name.Substring(0, musicFile.Name.Length - 4), musicFile.FullName));
            }

            return musicList;
        }

        internal static List<Sound> GetSoundsList()
        {
            string[] soundFiles = Directory.GetFiles(Config.StaticWorkDir + Config.SoundsDir, "*.wav");

            soundList.Clear();

            foreach (string file in soundFiles)
            {
                var soundFile = new FileInfo(file);
                soundList.Add(new Sound(soundFile.Name.Substring(0, soundFile.Name.Length - 4), soundFile.FullName));
            }

            return soundList;
        }
    }
}