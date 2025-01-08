using System.Collections.Generic;
using System.Linq;
using DTT.Utils.Extensions;
using Project.Configs.Game;
using Project.Configs.Ships;
using Project.Configs.Stats;
using Project.Interfaces.Data;
using Project.Systems.Stats;
using UnityEngine;
using YG;

namespace Project.Systems.Data
{
    public class GameDataService : IResourceStorageData,
        IPlayerStatsData,
        IAdvertismentData,
        ILevelSceneService,
        ILevelDataService,
        IScoreService
    {
        private const string SaveKey = nameof(SaveKey);

        private readonly GameData _gameData;
        private readonly ShipConfigSheet _shipConfigSheet;
        private readonly PlayerStatsProvider _statsProvider;

        public GameDataService(GameConfig config, ShipConfigSheet shipConfigSheet, StatsSheet statsSheet)
        {
            GameData data = YandexGame.savesData.GameData;

            _shipConfigSheet = shipConfigSheet;
            string defaultShipID = shipConfigSheet.Ships.First().ID;

            _gameData = data ?? new GameData(config.FirstLevelScene, defaultShipID);

            if (_gameData.CurrentScene.IsNullOrEmpty())
            {
                _gameData.CurrentScene = config.FirstLevelScene;
            }
            
            _statsProvider = new PlayerStatsProvider(this, statsSheet);
            
            LoadShipStats();
        }

        public List<GameResourceData> Storage => _gameData.StorageData;

        public List<PlayerStatData> StatsLevels => _gameData.PlayerStatsLevels;
        
        public string CurrentLevel => _gameData.CurrentScene;

        public bool IsAdHided { get => _gameData.IsAddHided; set => _gameData.IsAddHided = value; }

        public ShipConfig GetSelectedShip()
        {
            return _shipConfigSheet.GetShipConfigById(_gameData.SelectedShipID);
        }

        public void UpdateSelectedShip(string shipID)
        {
            _gameData.SelectedShipID = shipID;
            LoadShipStats();
            Save();
        }

        public void UpdateCurrentLevel(string levelName)
        {
            _gameData.CurrentScene = levelName;
            Save();
        }

        public LevelData GetLevelData(string levelName)
        {
            LevelData leveldata = _gameData.Levels.FirstOrDefault(l => l.LevelName == levelName);

            if (leveldata == null)
            {
                leveldata = new LevelData(levelName);

                _gameData.Levels.Add(leveldata);
            }

            return leveldata;
        }

        public void Save()
        {
            string selectedShip = _gameData.SelectedShipID;
            _gameData.ShipStatsLevels[selectedShip] = _statsProvider.SaveStats();
            
            YandexGame.savesData.GameData = _gameData;

            YandexGame.SaveProgress();
        }

        public int GetScore()
        {
            return _gameData.Score;
        }

        public void SetScore(int score)
        {
            _gameData.Score = score;
            Save();
        }

        private void LoadShipStats()
        {
            string shipID = _gameData.SelectedShipID;

            if (!_gameData.ShipStatsLevels.ContainsKey(shipID))
            {
                _gameData.ShipStatsLevels[shipID] = new List<PlayerStatData>(); // Новый корабль
            }

            // Передаем статы текущего корабля в провайдер
            _statsProvider.LoadStats(_gameData.ShipStatsLevels[shipID]);
        }
    }
}