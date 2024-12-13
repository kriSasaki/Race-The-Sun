using System.Collections;
using System.Collections.Generic;
using Project.Configs.Game;
using Project.Configs.UI;
using Project.Interfaces.Audio;
using UnityEngine;
using Zenject;

namespace Project.UI.Settings
{
    public class SettingsWindow : UiWindow
    {
        private UiConfigs _uiConfigs;
        private GameConfig _gameConfig;
        private IAudioService _audioService;
        
        [Inject]
        public void Construct(UiConfigs uiConfigs,GameConfig gameConfig, IAudioService audioService)
        {
            _uiConfigs = uiConfigs;
            _gameConfig = gameConfig;
            _audioService = audioService;
        }
        
        public void Open()
        {
            Show();
        }
        
        public override void Hide()
        {
            base.Hide();

            _audioService.PlayMusic(_gameConfig.MainMusic);
        }
    }
}