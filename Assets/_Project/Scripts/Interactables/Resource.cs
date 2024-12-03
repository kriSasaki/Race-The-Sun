using System;
using Ami.BroAudio;
using Project.Configs.GameResources;
using Project.Interfaces.Audio;
using Project.Interfaces.Resources;
using Project.Resource.View;
using Project.Spawner;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class Resource : MonoBehaviour, IPoolableResource
    {
        [SerializeField] private SoundID _lootSound;

        private ResourceConfig _config;
        private SphereCollider _resourceCollider;
        private ResourceView _view;
        private VfxSpawner _vfxSpawner;
        private IAudioService _audioService;

        public event Action<IResource> Collected;

        public ResourceConfig Config => _config;
        public ResourceView View => _view;
        public Collider ResourceCollider => _resourceCollider;
        public Transform Transform => transform;
        public VfxSpawner VfxSpawner => _vfxSpawner;
        public Vector3 Position => transform.position;
        public GameResourceAmount Loot => _config.Loot;
        public Vector3 SpawnPosition { get; private set; }

        public void Initialize(
            ResourceConfig config,
            VfxSpawner vfxSpawner,
            IAudioService audioService)
        {
            _config = config;
            _vfxSpawner = vfxSpawner;
            _audioService = audioService;
            _resourceCollider = GetComponent<SphereCollider>();
            
            SetSpawnPosition();
            
            _view = Instantiate(_config.View, transform);
            
            SetShipCollider();
            
            _view.Initialize(_vfxSpawner, _audioService);
        }

        public void Destroy()
        {
            Collected?.Invoke(this);
            _audioService.PlaySound(_lootSound);
            _resourceCollider.enabled = false;
        }

        private void SetSpawnPosition()
        {
            SpawnPosition = transform.position;
        }

        private void SetShipCollider()
        {
            _resourceCollider.radius = Mathf.Max(
                _view.Bounds.size.x,
                _view.Bounds.size.y,
                _view.Bounds.size.z) / 2f;
            _resourceCollider.center = _view.Bounds.center;
        }
    }
}