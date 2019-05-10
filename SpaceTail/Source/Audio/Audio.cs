using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceTail.Source.Audio
{
    abstract class Audio
    {
        protected abstract bool isPlaying { get; set; }

        public abstract string Name { get; set; }
        public abstract string Path { get; set; }

        public Audio()
        {
        }

        protected abstract void InitAudio();

        public abstract void Loop();

        public abstract void Play();

        public abstract void Stop();
    }
}
