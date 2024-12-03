using System;

namespace Project.Interfaces.Stats
{
    public interface IPlayerStats
    {
        public event Action StatsUpdated;

        int Battery { get; }
        int Speed { get; }
        int TurnSpeed { get; }
        int FloatTime { get; }
        int PickUpRange { get; }
        int XPMultiplier { get; }
    }
}