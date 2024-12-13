﻿using Ami.BroAudio;
using Project.Configs.Level;
using Project.General.View;
using Project.Interfaces.Audio;
using Project.Spawner;
using Project.Utils.Tweens;
using UnityEngine;
using Zenject;

namespace Project.Players.View
{
    public class PlayerView : ShipView
    {
        [SerializeField] private SoundID _hitSound;

        private IAudioService _audioService;
        private VfxSpawner _vfxSpawner;

        private void Start()
        {
            // _waterTrail.Play();
        }

        public override void TakeDamage(int damage)
        {
            _audioService.PlaySound(_hitSound);
            _vfxSpawner.SpawnExplosion(transform.position, transform);
            // _vfxSpawner.SpawnDamagePopup(transform.position, damage);
        }

        protected override void OnDie()
        {
            base.OnDie();

            // _waterTrail.Stop();
            // _waterTrail.Clear();
        }

        protected override void OnRessurect()
        {
            base.OnRessurect();

            // _waterTrail.Play();
        }

        [Inject]
        private void Construct(
            IAudioService audioService,
            VfxSpawner vfxSpawner)
        {
            _audioService = audioService;
            _vfxSpawner = vfxSpawner;
        }
    }
}