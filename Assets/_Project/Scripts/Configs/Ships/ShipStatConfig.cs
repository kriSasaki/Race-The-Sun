using System.Collections.Generic;
using Project.Configs.Stats;
using Project.Systems.Stats;
using UnityEngine;

namespace Project.Configs.Ships
{
    [CreateAssetMenu(fileName = "ShipStatsConfig", menuName = "Configs/ShipStatsConfig", order = 1)]
    public class ShipStatsConfig : ScriptableObject
    {
        [SerializeField] private List<StatConfig> _stats;

        public IEnumerable<StatConfig> Stats => _stats;

        public StatConfig GetStatConfig(StatType statType)
        {
            return _stats.Find(stat => stat.StatType == statType);
        }
    }
}