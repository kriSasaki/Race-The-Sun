using System.Collections.Generic;
using Project.Systems.Data;
using Project.Systems.Stats;

namespace Project.Interfaces.Data
{
    public interface IPlayerStatsProvider
    {
        public Dictionary<StatType, PlayerStat> LoadStats();
        public void LoadStats(List<PlayerStatData> statData);
        public List<PlayerStatData> SaveStats();

        public void UpdateStats();
    }
}