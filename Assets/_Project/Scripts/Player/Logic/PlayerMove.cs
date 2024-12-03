using System;
using Project.Interfaces.Stats;
using UnityEngine;
using Zenject;

namespace Project.Players.Logic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField, Range(30f, 120f)] private float _turnSpeed;

        private IPlayerStats _playerStats;
        private Player _player;
        private MoveHandler _moveHandler;

        public int MovementSpeed => _playerStats.Speed;
        public float TurnSpeed => _turnSpeed;

        private void Update()
        {
            if (_player.IsAlive == false || _player.CanMove == false)
                return;

            _moveHandler.ReadInput();
        }

        private void FixedUpdate()
        {
            if (_player.IsAlive == false || _player.CanMove == false)
                return;

            _moveHandler.Rotate();
            _moveHandler.Move();
        }


        [Inject]
        private void Construct(IPlayerStats playerStats, Player player, MoveHandler moveHandler)
        {
            _playerStats = playerStats;
            _player = player;
            _moveHandler = moveHandler;

            _moveHandler.Initialize(_playerRigidbody, _transform);
        }
    }
}