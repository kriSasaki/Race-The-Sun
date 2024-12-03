using System.Collections.Generic;
using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface IResourceStorageData : ISaveable
    {
        GameResourceData GetResourceData(string id);

        void UpdateResourceData(string id, int value);
    }
}