using System;
using System.Collections.Generic;
using Project.Interfaces.Data;
using Project.Interfaces.Stats;

namespace Project.Systems.Stats
{
    public class PlayerStats : IPlayerStats, IUpgradableStats
    {
        private IPlayerStatsProvider _provider;
        private Dictionary<StatType, PlayerStat> _stats;

        public PlayerStats(IPlayerStatsProvider provider)
        {
            _provider = provider;
            _stats = _provider.LoadStats();

            SetStatValues();
        }

        public event Action StatsUpdated;
        
        public int Battery { get; private set; }
        public int Speed { get; private set; }
        public int TurnSpeed { get; private set; }
        public int FloatTime { get; private set; }
        public int PickUpRange { get; private set; }
        public int XPMultiplier { get; private set; }

        public void UpgradeStat(StatType type)
        {
            _stats[type].LevelUp();
            UpdateStatsValues();
        }

        public void SetStatValue(StatType type, int value)
        {
            _stats[type].SetLevel(value);
            UpdateStatsValues();
        }

        public int GetStatValue(StatType type)
        {
            return _stats[type].GetValue();
        }

        public int GetStatLevel(StatType type)
        {
            return _stats[type].Level;
        }

        private void UpdateStatsValues()
        {
            SetStatValues();
            SaveStats();
            StatsUpdated?.Invoke();
        }

        private void SetStatValues()
        {
            Battery = GetStatValue(StatType.Battery);
            Speed = GetStatValue(StatType.Speed);
            TurnSpeed = GetStatValue(StatType.TurnSpeed);
            FloatTime = GetStatValue(StatType.FloatTime);
            PickUpRange = GetStatValue(StatType.PickUpRange);
            XPMultiplier = GetStatValue(StatType.XPMultiplier);
        }

        private void SaveStats()
        {
            _provider.UpdateStats();
        }
    }
}