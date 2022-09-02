using System;
using Common.Utils;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Entity
{
    public class EcsEntity
    {
        public int id;
        public EcsWorld world;
        public int[] components;

        public EcsEntity(EcsWorld world, int id, int[] components = null)
        {
            this.world = world;
            this.id = id;
            this.components = components;
        }

        public T GetComponent<T>() where T : IEcsComponent
        {
            var typeIndex = EcsComponentPoolIndex<T>.TypeIndex;
            EcsComponentsPool<T> pool;

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


            pool = world.GetPool<T>();

            var component = pool.Get(id);

            if (components == null)
            {
                components = new[] {typeIndex};
            }
            else
            {
                Array.Resize(ref components, components.Length + 1);
                components[^1] = typeIndex;
            }

            return component;
        }


        public void RemoveComponent<T>() where T : IEcsComponent
        {
            if (!HasComponent<T>()) return;

            var typeIndex = EcsComponentPoolIndex<T>.TypeIndex;


            if (!world.HasPool<T>())
            {
                return;
            }

            var pool = world.GetPool<T>();

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
                world.RemoveEntity(this);
                Debug.Log($"{id} don't have any components anymore and return to pool");
            }
        }

        public bool HasComponent<T>() where T : IEcsComponent
        {
            if (components == null || components.Length <= 0) return false;
            var typeIndex = EcsComponentPoolIndex<T>.TypeIndex;
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == typeIndex)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasComponent(int componentId)
        {
            if (components == null || components.Length <= 0) return false;
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == componentId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}