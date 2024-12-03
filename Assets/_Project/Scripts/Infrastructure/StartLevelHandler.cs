using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Configs.Game;
using Project.Interfaces.Audio;
using Project.Interfaces.SDK;
using Project.Players.Logic;
using Project.Systems.Cameras;
using Project.UI;
using Project.Spawner;
using UnityEngine;
using YG;
using Zenject;

namespace Project.Infrastructure
{
    public class StartLevelHandler : MonoBehaviour
    {
        [SerializeField] private float _unfadeCanvasDuration = 0.5f;

        private Player _player;
        private PlayerSpawner _playerSpawner;
        private CameraSystem _cameraSystem;
        private UiCanvas _uiCanvas;
        private IGameReadyService _gameReadyService;
        private GameConfig _gameConfig;
        private IAudioService _audioService;

        [Inject]
        private void Construct(
            Player player,
            PlayerSpawner playerSpawner,
            CameraSystem cameraSystem,
            UiCanvas uiCanvas,
            IGameReadyService gameReadyService,
            IAudioService audioService,
            GameConfig gameConfig)
        {
            _player = player;
            _playerSpawner = playerSpawner;
            _cameraSystem = cameraSystem;
            _uiCanvas = uiCanvas;
            _gameReadyService = gameReadyService;
            _gameConfig = gameConfig;
            _audioService = audioService;
        }

        private async UniTaskVoid Start()
        {
            _audioService.StopAudio();
            _audioService.PlayAmbience(_gameConfig.Ambience);
            _playerSpawner.Initialize();
            _player.DisableMove();
            DisableUi();

            await _cameraSystem.ShowOpeningAsync(destroyCancellationToken);

            _player.EnableMove();
            _gameReadyService.Call();
            YandexGame.GameplayStart();

            await _uiCanvas.EnableAsync(_unfadeCanvasDuration, destroyCancellationToken);

            await UniTask.WaitUntil(() => Input.anyKey);
            
            _audioService.PlayMusic(_gameConfig.MainMusic);
        }

        private void DisableUi()
        {
            _uiCanvas.Disable();
        }
    }
}