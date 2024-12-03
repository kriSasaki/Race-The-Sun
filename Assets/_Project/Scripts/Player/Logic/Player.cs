using System;
using Ami.BroAudio;
using Project.Interfaces.Audio;
using Project.Interfaces.Hold;
using Project.Interfaces.Stats;
using Project.Players.View;
using UnityEngine;
using Zenject;

namespace Project.Players.Logic
{
    public class Player : MonoBehaviour
    {
        private const int ZeroCharge = 0;
        
        [SerializeField] private PlayerView _view;
        [SerializeField] private SoundID _healSound;
        
        private IPlayerStats _playerStats;
        private IPlayerHold _playerHold;
        private IAudioService _audioService;
        private Rigidbody _rigidbody;
        
        private int _currentCharge;
        private bool _canMove = true;
        
        public event Action ChargeChanged;
        public event Action Died;
        
        
        
        public Vector3 Position => transform.position;
        public int CurrentCharge => _currentCharge;
        public int MaxCharge => _playerStats.Battery;
        public bool IsAlive => _currentCharge > 0;
        public bool CanMove => _canMove;
        
        private void Start()
        {
            ChargeChanged?.Invoke();
        }
        
        private void OnDestroy()
        {
            _playerStats.StatsUpdated -= OnStatsUpdated;
        }
        
        public void TakeDamage(int damage)
        {
            if (IsAlive == false)
                return;

            _currentCharge = Math.Max(_currentCharge - damage, ZeroCharge);

            ChargeChanged?.Invoke();
            _view.TakeDamage(damage);

            if (IsAlive == false)
                Died?.Invoke();
        }
        
        public void Recharge()
        {
            if (_currentCharge != MaxCharge)
                _audioService.PlaySound(_healSound);
        
            _currentCharge = MaxCharge;
        
            ChargeChanged?.Invoke();
        }
        
        public void UnloadHold()
        {
            _playerHold.LoadToStorage();
        }
        
        public void EnableMove()
        {
            _canMove = true;
        }
        
        public void DisableMove()
        {
            _canMove = false;
        }
        
        public void SetPosition(Vector3 at)
        {
            _rigidbody.MovePosition(at);
        }
        
        [Inject]
        private void Construct(IPlayerStats playerStats, IPlayerHold playerHold, IAudioService audioService)
        {
            _playerStats = playerStats;
            _playerHold = playerHold;
            _audioService = audioService;
            _currentCharge = MaxCharge;
            _rigidbody = GetComponent<Rigidbody>();

            _playerStats.StatsUpdated += OnStatsUpdated;
        }
        
        private void OnStatsUpdated()
        {
            Recharge();
        }
    }
}