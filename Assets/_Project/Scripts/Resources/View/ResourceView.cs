using Ami.BroAudio;
using NaughtyAttributes;
using Project.Interfaces.Audio;
using Project.Spawner;
using UnityEngine;

namespace Project.Resource.View
{
    public class ResourceView : InteractableView
    {
        [SerializeField] private MeshFilter _resourceMesh;
        [SerializeField] private SoundID _disappearingSound;
        [SerializeField, ShowAssetPreview] private Sprite _icon;

        private VfxSpawner _vfxSpawner;
        private IAudioService _audioService;

        public Sprite Icon => _icon;
        public Bounds Bounds => _resourceMesh.sharedMesh.bounds;

        public void Initialize(
            VfxSpawner vfxSpawner,
            IAudioService audioService)
        {
            _vfxSpawner = vfxSpawner;
            _audioService = audioService;
        }

        public override void Disappear()
        {
            _audioService.PlaySound(_disappearingSound);
        }
    }
}