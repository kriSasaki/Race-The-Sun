using System;
using Project.Interfaces.Hold;
using Project.Interfaces.Resources;

namespace Project.Players.Logic
{
    public class PlayerLootController : IDisposable
    {
        private readonly IPlayerHold _playerHold;
        private readonly PlayerResourceCollector _playerResourceCollector;

        public PlayerLootController(IPlayerHold playerHold, PlayerResourceCollector playerResourceCollector)
        {
            _playerHold = playerHold;
            _playerResourceCollector = playerResourceCollector;

            _playerResourceCollector.ResourceCollected += OnResourceCollected;
        }

        public void Dispose()
        {
            _playerResourceCollector.ResourceCollected -= OnResourceCollected;
        }

        private void OnResourceCollected(IResource resource)
        {
            _playerHold.AddResource(resource.Loot);
        }
    }
}