using System;

namespace CustomEcsBase.Components.Interfaces
{
    public interface IEcsComponentPool
    {
        int Id { get; }
        event Action<int, int> AddedNewComponents; // param poolId, entityId
        event Action<int, int> RemoveComponents; // param poolId, entityId

        void Remove(int entityId);
        void Reset();
    }
}