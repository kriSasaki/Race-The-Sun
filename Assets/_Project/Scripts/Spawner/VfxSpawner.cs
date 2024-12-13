using Project.Interfaces.Audio;
using Project.Utils;
using UnityEngine;
using Zenject;

namespace Project.Spawner
{
    public class VfxSpawner : MonoBehaviour
    {
        private readonly Vector3 ExplotionOffset = Vector3.up;

        [SerializeField] private ParticleSystem _explosionPrefab;

        private VFXPool<ParticleSystem> _smokePool;
        private VFXPool<ParticleSystem> _explosionPool;

        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            // _smokePool = new VFXPool<ParticleSystem>(_cannonSmokePrefab);
            _explosionPool = new VFXPool<ParticleSystem>(_explosionPrefab);
            // _damagePopupPool = new VFXPool<DamagePopup>(_damagePopupPrefab);
            // _projectilePool = new VFXPool<Projectile>(_projectilePrefab);
            _audioService = audioService;
        }

        public void SpawnCannonSmoke(Collider shooterCollider, Vector3 targetPosition)
        {
            Vector3 spawnPosition = shooterCollider.ClosestPoint(targetPosition);
            SpawnCannonSmoke(spawnPosition, targetPosition);
        }

        public void SpawnCannonSmoke(Vector3 from, Vector3 towards)
        {
            var smoke = _smokePool.Get();
            smoke.transform.SetPositionAndRotation(from, Quaternion.LookRotation(towards - from));
            smoke.Play();
        }

        public void SpawnExplosion(Vector3 atPosition, Transform parent = null)
        {
            var explotion = _explosionPool.Get();
            explotion.transform.SetPositionAndRotation(atPosition + ExplotionOffset, Quaternion.identity);
            explotion.transform.SetParent(parent);
            explotion.Play();
        }
    }
}