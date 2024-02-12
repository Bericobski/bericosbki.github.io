using SFML.Audio;

namespace Juego_Finale
{
    public class SoundEffect
    {
        private readonly SoundBuffer buffer;
        private readonly Sound sound;

        public SoundStatus Status => sound.Status;


        public SoundEffect(string filePath)
        {
            buffer = new SoundBuffer(filePath);
            sound = new Sound(buffer);
        }

        public void Play() => sound.Play();
        public void Pause() => sound.Pause();
        public void Stop() => sound.Stop();
    }
}
