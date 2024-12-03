using System.Collections.Generic;
using Project.Configs.GameResources;
using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface IPlayerStorage
    {
        void AddResource(GameResource gameResource, int amount);

        void AddResource(GameResourceAmount gameResourceAmount);

        void AddResource(List<GameResourceAmount> gameResourcesAmounts);

        bool TrySpendResource(GameResource gameResource, int amount);

        bool TrySpendResource(GameResourceAmount gameResourceAmount);

        bool TrySpendResource(List<GameResourceAmount> gameResourcesAmounts);

        bool CanSpend(GameResource gameResource, int amount);

        bool CanSpend(GameResourceAmount gameResourceAmount);

        bool CanSpend(List<GameResourceAmount> gameResourcesAmounts);
    }
}