using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceTail.Source.Audio
{
    class Sound : Audio
    {
        protected override bool isPlaying { get; set; }
        public override string Name { get; set; }
        public override string Path { get; set; }

        AudioFileReader audioFile;
        WaveOutEvent outputDevice;
        Thread audioThread;

        public Sound(string name, string path)
        {
            Name = name;
            Path = path;
            InitAudio();
        }

        protected override void InitAudio()
        {
            
            audioFile = new AudioFileReader(Path);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
        }

        public override void Loop()
        {
            if (!isPlaying)
            {
                isPlaying = true;
                audioThread = new Thread(() =>
                {
                    using (var audioFile = new AudioFileReader(Path))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        while (true)
                        {
                            outputDevice.Play();
                            while (outputDevice.PlaybackState != PlaybackState.Stopped)
                            {
                                Thread.Sleep(100);
                            }
                        }
                    }
                });
                audioThread.Start();
            }
        }

        public override void Play()
        {
            if (!isPlaying)
            {
                isPlaying = true;
                audioThread = new Thread(() =>
                {
                    using (var audioFile = new AudioFileReader(Path))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState != PlaybackState.Stopped)
                        {
                            Thread.Sleep(100);
                        }
                        isPlaying = false;
                    }
                });
                audioThread.Start();
            }
        }

        public override void Stop()
        {
            if (isPlaying)
            {
                audioThread.Abort();
                isPlaying = false;
            }
        }
    }
}
