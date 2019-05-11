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

        public Music(string name, string path)
        {
            Name = name;
            Path = path;
            InitAudio();
        }

        protected override void InitAudio()
        {
            using (vorbisStream = new VorbisWaveReader(Path))
            using (waveOut = new WaveOutEvent())
            {
                waveOut.Init(vorbisStream);
            }
        }

        public async override void Loop()
        {
            if (!isPlaying)
            {
                isPlaying = true;

                await Task.Run(() =>
                {
                    using (var vorbisStream = new VorbisWaveReader(Path))
                    using (var loop = new LoopStream(vorbisStream))
                    using (var waveOut = new WaveOutEvent())
                    {
                        waveOut.Init(loop);
                        while (isPlaying != false)
                        {
                            waveOut.Play();
                            while (waveOut.PlaybackState != PlaybackState.Stopped
                                    && isPlaying != false)
                            {
                                Thread.Sleep(100);
                            }
                            waveOut.Dispose();
                        }
                        
                    }
                });
            }
        }

        public async override void Play()
        {
            if (!isPlaying)
            {
                isPlaying = true;

                await Task.Run(() => {
                    using (var vorbisStream = new VorbisWaveReader(Path))
                    using (var waveOut = new NAudio.Wave.WaveOutEvent())
                    {
                        waveOut.Init(vorbisStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState != PlaybackState.Stopped
                                && isPlaying != false)
                        {
                            Thread.Sleep(100);
                        }
                        waveOut.Dispose();
                        isPlaying = false;
                    }
                });
            }
        }

        public override void Stop()
        {
            if (isPlaying)
            {
                isPlaying = false;
            }
        }
    }
}
