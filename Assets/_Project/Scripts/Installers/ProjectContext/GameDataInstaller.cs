using Project.Configs.Game;
using Project.Configs.GameResources;
using Project.Configs.Ships;
using Project.Configs.ShopItems;
using Project.Configs.Stats;
using Project.Configs.UI;
using Project.Interfaces.Data;
using Project.Systems.Data;
using UnityEngine;
using Zenject;

namespace Project.Installers.ProjectContext
{
    public class GameDataInstaller : MonoInstaller
    {
        [SerializeField] private GameResourcesSheet _resourcesSheet;
        [SerializeField] private ShopItemsConfigs _shopItemsCongigs;
        [SerializeField] private StatsSheet _statsSheet;
        [SerializeField] private ShipConfigSheet _shipConfigSheet;
        [SerializeField] private UiConfigs _uiConfigs;
        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameResourcesSheet>().FromInstance(_resourcesSheet);
            Container.Bind<StatsSheet>().FromInstance(_statsSheet);
            Container.Bind<ShipConfigSheet>().FromInstance(_shipConfigSheet);
            Container.Bind<ShopItemsConfigs>().FromInstance(_shopItemsCongigs);
            Container.Bind<UiConfigs>().FromInstance(_uiConfigs);
            Container.Bind<GameConfig>().FromInstance(_gameConfig);

            Container.BindInterfacesTo<GameDataService>().AsSingle().NonLazy();

            Container.Bind<IResourceStorageProvider>().To<ResourceStorageProvider>().AsSingle();
            Container.Bind<IPlayerStatsProvider>().To<PlayerStatsProvider>().AsSingle();
        }
    }
}