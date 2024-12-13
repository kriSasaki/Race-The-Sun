﻿using System.Collections.Generic;
using System.Linq;
using DTT.Utils.Extensions;
using Project.Configs.Game;
using Project.Interfaces.Data;
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

        public GameDataService(GameConfig config)
        {
            GameData data = YandexGame.savesData.GameData;
            
            _gameData = data ?? new GameData(config.FirstLevelScene);

            if (_gameData.CurrentScene.IsNullOrEmpty())
            {
                _gameData.CurrentScene = config.FirstLevelScene;
            }
        }

        public List<GameResourceData> Storage => _gameData.StorageData;

        public List<PlayerStatData> StatsLevels => _gameData.PlayerStatsLevels;
        
        public string CurrentLevel => _gameData.CurrentScene;

        public bool IsAdHided { get => _gameData.IsAddHided; set => _gameData.IsAddHided = value; }

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
    }
}