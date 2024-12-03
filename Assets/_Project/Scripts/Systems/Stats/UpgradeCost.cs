using Project.Configs.GameResources;
using Project.Configs.Stats;
using Project.Systems.Data;
using Project.Utils;
using UnityEngine;

namespace Project.Systems.Stats
{
    [System.Serializable]
    public class UpgradeCost
    {
        [SerializeField] private GameResource _currency;

        [field: SerializeField] protected int MinCost { get; private set; }
        [field: SerializeField] protected int MaxCost { get; private set; }

        public GameResourceAmount GetCost(StatConfig stat, int level)
        {
            int cost = ComputeCost(stat, level);

            return new GameResourceAmount(_currency, cost);
        }

        protected virtual int ComputeCost(StatConfig statConfig, int level)
        {
            return (int)ExtendedMath.Remap(level, statConfig.MinLevel, statConfig.MaxLevel, MinCost, MaxCost);
        }
    }
}