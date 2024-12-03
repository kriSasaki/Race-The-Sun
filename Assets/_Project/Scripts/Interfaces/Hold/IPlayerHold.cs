using System;
using Project.Systems.Data;

namespace Project.Interfaces.Hold
{
    public interface IPlayerHold
    {
        event Action<int> CargoChanged;

        public bool IsEmpty { get; }

        void AddResource(GameResourceAmount gameResourceAmount);
        void LoadToStorage();
    }
}