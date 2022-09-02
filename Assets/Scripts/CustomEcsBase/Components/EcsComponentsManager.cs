using System;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;

namespace CustomEcsBase.Components
{
    public class EcsComponentsManager : IEcsComponentsManager
    {
        private IEcsComponentPool[] componentPools;

        public bool HasPool<T>() where T : IEcsComponent => EcsComponentPoolIndex<T>.TypeIndex < componentPools.Length;

        public EcsComponentsPool<T> GetPool<T>() where T : IEcsComponent
        {
            var typeIndex = EcsComponentPoolIndex<T>.TypeIndex;
            if (componentPools == null)
            {
                componentPools = new IEcsComponentPool[0];
            }

            if (typeIndex < componentPools.Length)
            {
                var pool = componentPools[typeIndex];

                if (pool == null)
                {
                    pool = new EcsComponentsPool<T>();
                }

                return (EcsComponentsPool<T>) pool;
            }

            var newSize = typeIndex + 1;

            if (typeIndex == componentPools.Length)
            {
                newSize = componentPools.Length + 1;
            }

            Array.Resize(ref componentPools, newSize);
            var newPool = new EcsComponentsPool<T>();
            componentPools[typeIndex] = newPool;

            return newPool;
        }

        public void Dispose()
        {
            if (componentPools != null)
            {
                for (int i = 0; i < componentPools.Length; i++)
                {
                    componentPools[i].Reset();
                }

                componentPools = null;
            }
        }
    }
}