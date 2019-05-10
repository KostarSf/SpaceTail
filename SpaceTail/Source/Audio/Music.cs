using NAudio.Vorbis;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceTail.Source.Audio
{
    class Music : Audio
    {
        public override string Name { get; set; }
        public override string Path { get; set; }
        protected override bool isPlaying { get; set; }

        VorbisWaveReader vorbisStream;
        WaveOutEvent waveOut;
        Thread musicThread;

        public Music(string name, string path)
        {
            Name = name;
            Path = path;
            InitAudio();
        }

        protected override void InitAudio()
        {
            vorbisStream = new VorbisWaveReader(Path);
            waveOut = new WaveOutEvent();
            waveOut.Init(vorbisStream);
        }

        public override void Loop()
        {
            if (!isPlaying)
            {
                isPlaying = true;
                musicThread = new Thread(() =>
                {
                    using (var vorbisStream = new VorbisWaveReader(Path))
                    using (var loop = new LoopStream(vorbisStream))
                    using (var waveOut = new WaveOutEvent())
                    {
                        waveOut.Init(loop);
                        while (true)
                        {
                            waveOut.Play();
                            while (waveOut.PlaybackState != PlaybackState.Stopped)
                            {
                                Thread.Sleep(100);
                            }
                        }
                    }
                });
                musicThread.IsBackground = true;
                musicThread.Start();
            }
        }

        public override void Play()
        {
            if (!isPlaying)
            {
                isPlaying = true;
                musicThread = new Thread(() =>
                {
                    using (var vorbisStream = new VorbisWaveReader(Path))
                    using (var waveOut = new NAudio.Wave.WaveOutEvent())
                    {
                        waveOut.Init(vorbisStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState != PlaybackState.Stopped)
                        {
                            Thread.Sleep(100);
                        }
                        isPlaying = false;
                    }
                });
                musicThread.IsBackground = true;
                musicThread.Start();
            }
        }

        public override void Stop()
        {
            if (isPlaying)
            {
                musicThread.Abort();
                isPlaying = false;
            }
        }
    }
}
