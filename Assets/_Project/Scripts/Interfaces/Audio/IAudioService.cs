using Ami.BroAudio;

namespace Project.Interfaces.Audio
{
    public interface IAudioService
    {
        void PlaySound(SoundID id);

        void PlayMusic(SoundID id);

        void PlayAmbience(SoundID id);

        void MuteAudio();

        void UnmuteAudio();

        void PauseAudio();

        void UnpauseAudio();

        void StopAudio();
        void SetMusicLevel(float musicLevel);
        public void SetEffectsVolume(float effectsVolume);
    }
}