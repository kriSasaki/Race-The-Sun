﻿using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Project.Systems.Stats;
using UnityEngine;

namespace Project.Configs.Stats
{
    [CreateAssetMenu(fileName = "StatsSheet", menuName = "Configs/Stats/StatSheet", order = 1)]
    public class StatsSheet : ScriptableObject
    {
        [SerializeField, Expandable] private List<StatConfig> _stats;

        public IEnumerable<StatConfig> Stats => _stats;

        public StatConfig GetStatConfig(StatType statType)
        {
            return _stats.First(s => s.StatType == statType);
        }
    }
}