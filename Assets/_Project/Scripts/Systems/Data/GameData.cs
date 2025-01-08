using System.Collections.Generic;

namespace Project.Systems.Data
{
    [System.Serializable]
    public class GameData
    {
        public List<GameResourceData> StorageData = new List<GameResourceData>();
        public List<PlayerStatData> PlayerStatsLevels = new List<PlayerStatData>();
        public List<LevelData> Levels = new List<LevelData>();
        public Dictionary<string, List<PlayerStatData>> ShipStatsLevels = new();

        public bool IsAddHided = false;
        public string CurrentScene;
        public string SelectedShipID;
        public int Score = 0;

        public GameData(string sceneName, string defaultShipID) 
        {
            CurrentScene = sceneName;
            SelectedShipID = defaultShipID;

            if (ShipStatsLevels.ContainsKey(defaultShipID) == false)
            {
                ShipStatsLevels[defaultShipID] = new List<PlayerStatData>();
            }
        }
    }
}