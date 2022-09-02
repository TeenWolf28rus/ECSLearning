using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.World;

namespace CustomEcsBase.Filter.Filters.ComponentsFilter
{
    public abstract class EcsBaseComponentsFilter : EcsBaseFilter
    {
        public abstract int ComponentsCount { get; }
        
        protected abstract void ProcessAddNewToPool(int poolId, int entityId);
        protected abstract void ProcessRemoveFromPool(int poolId, int entityId);
        protected abstract void FillComponents();

        protected void InitPool<T>(ref EcsComponentsPool<T> pool) where T : IEcsComponent
        {
            pool = world.GetPool<T>();
            pool.AddedNewComponents -= ProcessAddNewToPool;
            pool.AddedNewComponents += ProcessAddNewToPool;

            pool.RemoveComponents -= ProcessRemoveFromPool;
            pool.RemoveComponents += ProcessRemoveFromPool;
        }

        protected bool TryRemoveComponent<T>(int entityId, ref List<T> componments) where T : IEcsComponent
        {
            for (int i = 0; i < componments.Count; i++)
            {
                if (componments[i].EntityId == entityId)
                {
                    componments.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
    }
}