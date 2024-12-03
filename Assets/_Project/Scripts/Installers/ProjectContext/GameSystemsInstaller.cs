using Project.Systems.Stats;
using Project.Systems.Storage;
using UnityEngine;
using YG;
using Zenject;

namespace Project.Installers.ProjectContext
{
    public class GameSystemsInstaller : MonoInstaller
    {
        [SerializeField] private YandexGame _yandexPrefab;

        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.Bind<YandexGame>().FromComponentInNewPrefab(_yandexPrefab).AsSingle().NonLazy();
#endif

            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInterfacesTo<PlayerStorage>().AsSingle();
        }
    }
}