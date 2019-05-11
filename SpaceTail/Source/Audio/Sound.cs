using NAudio.Wave;
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

        public Sound(string name, string path)
        {
            Name = name;
            Path = path;
            InitAudio();
        }

        protected override void InitAudio()
        {

            using (audioFile = new AudioFileReader(Path))
            using (outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
            }
        }

        public async override void Loop()
        {
            if (!isPlaying)
            {
                isPlaying = true;

                await Task.Run(() =>
                {
                    using (var audioFile = new AudioFileReader(Path))
                    using (var loop = new LoopStream(audioFile))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(loop);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState != PlaybackState.Stopped
                                && isPlaying != false)
                        {
                            Thread.Sleep(100);
                        }
                        outputDevice.Dispose();
                    }
                });
            }
        }

        public async override void Play()
        {
            if (!isPlaying)
            {
                isPlaying = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(100);
                    isPlaying = false;
                });

                await Task.Run(() =>
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
                        outputDevice.Dispose();
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
