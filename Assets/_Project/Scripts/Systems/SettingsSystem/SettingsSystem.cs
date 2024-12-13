using System;
using Project.Interfaces.Audio;
using Project.UI.Settings;
using UnityEngine;
using Zenject;

namespace Project.Systems.Settings
{
    public class SettingsSystem : IInitializable, IDisposable
    {
        private const string MuteKey = nameof(MuteKey);
        
        private readonly IAudioService _audioService;
        private readonly SettingsWindow _settingsWindow;
        private readonly SettingsButton _settingsButton;

        private float _musicLevel;
        private float _effectsLevel;

        public SettingsSystem(
            IAudioService audioService,
            SettingsWindow settingsWindow,
            SettingsButton settingsButton)
        {
            _audioService = audioService;
            _settingsWindow = settingsWindow;
            _settingsButton = settingsButton;
        }

        public void Initialize()
        {
            _settingsButton.Bind(OpenSettings);
            _musicLevel = PlayerPrefs.GetFloat(MuteKey);
            UpdateAudioState();
        }
        
        public void Dispose()
        {
        }
        
        private void OpenSettings()
        {
            _settingsWindow.Open();
        }

        private void UpdateAudioState()
        {
            _audioService.SetMusicLevel(_musicLevel);
        }
    }
}