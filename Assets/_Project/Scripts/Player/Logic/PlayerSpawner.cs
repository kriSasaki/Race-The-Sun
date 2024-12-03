using System;
using Project.Interfaces.Data;
using Project.Systems.Data;
using Project.Interactables;
using UnityEngine.SceneManagement;

namespace Project.Players.Logic
{
    public class PlayerSpawner : IDisposable
    {
        private readonly Player _player;
        private readonly LevelData _levelData;

        public PlayerSpawner(
            Player player,
            ILevelDataService levelDataService)
        {
            _player = player;
            _levelData = levelDataService.GetLevelData(SceneManager.GetActiveScene().name);
        }

        public void Initialize()
        {
            // if (_levelData.IsReachedPirateBay)
            //     _player.SetPosition(_pirateBay.PlayerRessurectPoint.position);
            // else
            //     _pirateBay.ReachedByPlayer += OnReachedPirateBay;
        }

        public void Dispose()
        {
            // _pirateBay.ReachedByPlayer -= OnReachedPirateBay;
        }

        private void OnReachedPirateBay()
        {
            // _levelData.ReachPirateBay();
            // _pirateBay.ReachedByPlayer -= OnReachedPirateBay;
        }
    }
}