using System;

namespace CustomEcsBase.Components.Interfaces
{
    public interface ICEcsComponentPool
    {
        int Id { get; }
        event Action<int> ComponentsUpdated;
        
        void Remove(int entityId);


        void Reset();
    }
}