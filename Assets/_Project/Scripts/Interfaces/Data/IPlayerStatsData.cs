using System.Collections.Generic;
using Project.Systems.Data;
using Project.Systems.Stats;

namespace Project.Interfaces.Data
{
    public interface IPlayerStatsData : ISaveable
    {
        PlayerStatData GetPlayerStatData(StatType statType);

        void UpdateStatData(StatType type, int level);
    }
}