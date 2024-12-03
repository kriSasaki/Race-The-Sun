using System;
using Project.Interfaces.Resources;
using Project.Interfaces.Stats;
using UnityEngine;
using Zenject;

namespace Project.Players.Logic
{
    public class PlayerResourceCollector : MonoBehaviour
    {
        private SphereCollider _detectZone;
        private IPlayerStats _playerStats;
        
        public event Action<IResource> ResourceCollected;

        public int PickUpRange => _playerStats.PickUpRange;

        private void Start()
        {
            SetLootZone();
        }

        private void OnEnable()
        {
            _detectZone.enabled = true;
        }

        private void OnDisable()
        {
            _detectZone.enabled = false;
        }
        
        private void OnDestroy()
        {
            _playerStats.StatsUpdated -= OnStatsUpdated;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IResource resource))
            {
                OnResourceCollected(resource);
                resource.Destroy();
            }
        }
        
        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _detectZone = GetComponent<SphereCollider>();
            _playerStats = playerStats;
            _playerStats.StatsUpdated += OnStatsUpdated;
        }

        private void OnResourceCollected(IResource resource)
        {
            ResourceCollected?.Invoke(resource);
        }

        private void SetLootZone()
        {
            _detectZone.radius = PickUpRange;
        }

        private void OnStatsUpdated()
        {
            SetLootZone();
        }
    }
}