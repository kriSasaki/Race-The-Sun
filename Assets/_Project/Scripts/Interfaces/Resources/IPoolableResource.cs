using UnityEngine;

namespace Project.Interfaces.Resources
{
    public interface IPoolableResource : IResource
    {
        Transform Transform { get; }
    }
}
