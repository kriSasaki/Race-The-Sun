using System.Collections.Generic;
using Lean.Localization;
using NaughtyAttributes;
using Project.Systems.Data;
using Project.Systems.Stats;
using Project.Utils;
using UnityEngine;

namespace Project.Configs.Stats
{
    [CreateAssetMenu(fileName = "StatConfig", menuName = "Configs/Stats/StatConfig")]

    public class StatConfig : ScriptableObject
    {
        [SerializeField] private UpgradeCost _primaryCost;

        [SerializeField] private string _name;
        [SerializeField] private string _description;

        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public bool IsUpgradeable { get; private set; } 
        [field: SerializeField, Min(0)] public int MinValue { get; private set; }
        [field: SerializeField, Min(0)] public int MaxValue { get; private set; }
        [field: SerializeField, Min(0)] public int MaxLevel { get; private set; }

        public string Name => _name;
        public string Description => _description;
        public int MinLevel => 1;

        public int GetValue(int level)
        {
            return (int)ExtendedMath.Remap(level, MinLevel, MaxLevel, MinValue, MaxValue);
        }

        public List<GameResourceAmount> GetUpgradePrice(int currentLevel)
        {
            List<GameResourceAmount> price = new() { _primaryCost.GetCost(this, currentLevel) };

            return price;
        }

        public bool IsMaxLevel(int currentLevel)
        {
            return currentLevel == MaxLevel;
        }
    }
}