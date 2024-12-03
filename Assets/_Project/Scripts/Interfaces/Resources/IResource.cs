using System;
using Project.Configs.GameResources;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Interfaces.Resources
{
    public interface IResource
    {
        event Action<IResource> Collected;
        
        Vector3 Position { get; }
        public GameResourceAmount Loot { get; }
        public ResourceConfig Config { get; }

        void Destroy();
    }
}