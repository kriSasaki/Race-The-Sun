using Project.Resource.View;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Configs.GameResources
{
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField, Range(30f, 100f)] private float _rotationSpeed;
        [SerializeField] private ResourceView _resourceView;
        [SerializeField] private GameResourceAmount _loot;

        public ResourceView View => _resourceView;
        public GameResourceAmount Loot => _loot;
        public Sprite Icon => _resourceView.Icon;
    }
}