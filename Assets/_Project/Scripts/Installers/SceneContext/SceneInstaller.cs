using Cinemachine;
using Project.Configs.Level;
using Project.Players.Logic;
using Project.Players.View;
using Project.Spawner;
using Project.Systems.Cameras;
using Project.Systems.Leaderboard;
using Project.Systems.Shop;
using Project.Systems.Storage;
using Project.Systems.Stats;
using Project.Systems.Upgrades;
using Project.UI;
using Project.UI.Leaderboard;
using Project.UI.Reward;
using Project.UI.Shop;
using Project.UI.Upgrades;
using UnityEngine;
using Zenject;

namespace Project.Installers.SceneContext
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private VfxSpawner _vfxSpawner;
        
        public LevelConfig LevelConfig => _levelConfig;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindSpawners();
            BindUI();
            BindSystems();
            BindPlayer();
        }

        private void BindConfigs()
        {
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        }

        private void BindSpawners()
        {
            Container.Bind<VfxSpawner>().FromComponentInNewPrefab(_vfxSpawner).AsSingle();
            Container.Bind<ShopItemFactory>().AsSingle();
        }

        private void BindSystems()
        {
            Container.Bind<CinemachineBrain>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<ShopSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<UpgradeSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LeaderboardSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<CameraSystem>().FromComponentInHierarchy().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<ShopWindow>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ShopButton>().FromComponentInHierarchy().AsSingle();

            Container.Bind<UpgradeWindow>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UpgradeButton>().FromComponentInHierarchy().AsSingle();

            Container.Bind<LeaderboardWindow>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LeaderboardButton>().FromComponentInHierarchy().AsSingle();

            Container.Bind<RewardView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<NextLevelWindow>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerDeathWindow>().FromComponentInHierarchy().AsSingle();

            Container.Bind<UiCanvas>().FromComponentInHierarchy().AsSingle();
        }

        private void BindPlayer()
        {
            BindMoveHandler();
            
            Container.BindInterfacesTo<PlayerHold>().AsSingle();
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerResourceCollector>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<PlayerLootController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerSpawner>().FromNew().AsSingle().NonLazy();
        }

        private void BindMoveHandler()
        {
            Container.Bind<MoveHandler>().FromNew().AsSingle();
        }
    }
}