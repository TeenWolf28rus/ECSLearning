using System;
using Common.Utils;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Entity
{
    public struct CEcsEntity
    {
        public int id;
        public CEcsWorld world;
        public int[] components;

        public CEcsEntity(CEcsWorld world, int id, int[] components = null)
        {
            this.world = world;
            this.id = id;
            this.components = components;
        }

        public T GetComponent<T>() where T : ICEcsComponent
        {
            var typeIndex = CEcsComponentPoolIndex<T>.TypeIndex;
            CEcsComponentsPool<T> pool;


            if (HasComponent<T>())
            {
                pool = world.GetPool<T>();
                if (pool != null)
                {
                    return pool.Get(id);
                }

                Debug.LogError("Enemy store component but component pool does not exist");
                return default;
            }


            var componentPools = world.ComponentPools;

            if (typeIndex < componentPools.Count)
            {
                pool = (CEcsComponentsPool<T>)componentPools[typeIndex];
            }
            else
            {
                pool = new CEcsComponentsPool<T>();
                componentPools.Add(pool);
            }

            var component = pool.Get(id);

            if (components == null)
            {
                components = new[] { typeIndex };
            }
            else
            {
                Array.Resize(ref components, components.Length);
                components[^1] = typeIndex;
            }

            return component;
        }


        public void RemoveComponent<T>() where T : ICEcsComponent
        {
            if (!HasComponent<T>()) return;

            var typeIndex = CEcsComponentPoolIndex<T>.TypeIndex;
            var componentPools = world.ComponentPools;

            if (typeIndex >= componentPools.Count)
            {
                return;
            }

            var pool = componentPools[typeIndex];
            pool.Remove(id);
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == typeIndex)
                {
                    ArrayUtils.RemoveAt(ref components, i);
                    break;
                }
            }

            if (components.Length <= 0)
            {
                world.ReturnToPool(this);
                Debug.Log($"{id} dont have any components anymore and return to pool");
            }
        }

        public bool HasComponent<T>() where T : ICEcsComponent
        {
            if (components == null || components.Length <= 0) return false;
            var typeIndex = CEcsComponentPoolIndex<T>.TypeIndex;
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == typeIndex)
                {
                    return true;
                }
            }

            return false;
        }
    }
}