using System.Collections.Generic;
using System.Linq;
using Project.Systems.Stats;
using UnityEngine;

namespace Project.Systems.Data
{
    [System.Serializable]
    public class GameData
    {
        private const int InitialStatLevel = 1;

        [SerializeField] private List<GameResourceData> _currencyData = new List<GameResourceData>();
        [SerializeField] private List<PlayerStatData> _playerStatsLevels = new List<PlayerStatData>();
        [SerializeField] private List<LevelData> _levels = new List<LevelData>();
        [SerializeField] private string _currentScene;
        [SerializeField] private bool _isAddHided = false;
        [SerializeField] private int _score = 0;

        public GameData(string sceneName)
        {
            _currentScene = sceneName;
        }

        public bool IsAddHided => _isAddHided;
        public string CurrentScene => _currentScene;

        public GameResourceData GetResourceData(string id)
        {
            return _currencyData.FirstOrDefault(r => r.ID == id);
        }

        public void UpdateResourceData(string id, int value)
        {
            GameResourceData data = _currencyData.FirstOrDefault(r => r.ID == id);

            if (data != null)
            {
                data.ChangeResourceAmount(value);
            }
            else
            {
                _currencyData.Add(new GameResourceData(id, value));
            }
        }

        public PlayerStatData GetPlayerStatData(StatType statType)
        {
            PlayerStatData playerStatData = _playerStatsLevels.FirstOrDefault(s => s.StatType == statType);

            if (playerStatData == null)
            {
                playerStatData = new PlayerStatData(statType, InitialStatLevel);
                _playerStatsLevels.Add(playerStatData);
            }

            return playerStatData;
        }

        public void UpdateStatData(StatType type, int level)
        {
            PlayerStatData data = _playerStatsLevels.FirstOrDefault(s => s.StatType == type);

            if (data != null)
            {
                data.SetLevel(level);
            }
            else
            {
                var statData = new PlayerStatData(type, level);
                _playerStatsLevels.Add(statData);
            }
        }

        public LevelData GetLevelData(string levelName)
        {
            LevelData leveldata = _levels.FirstOrDefault(l => l.LevelName == levelName);

            if (leveldata == null)
            {
                leveldata = new LevelData(levelName);

                _levels.Add(leveldata);
            }

            return leveldata;
        }

        public void RemoveAd()
        {
            _isAddHided = true;
        }

        public void UpdateCurrentLevel(string levelName)
        {
            _currentScene = levelName;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}