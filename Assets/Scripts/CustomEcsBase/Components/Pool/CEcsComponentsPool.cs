using System;
using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;

namespace CustomEcsBase.Components.Pool
{
    public class CEcsComponentsPool<T> : ICEcsComponentPool where T : ICEcsComponent
    {
        private int id = -1;
        private Type type;
        private List<T> pooledComponents = new List<T>();
        private List<T> activeComponents = new List<T>();

        public int Id => id;

        public event Action<int> ComponentsUpdated;

        public CEcsComponentsPool()
        {
            id = CEcsComponentPoolIndex<T>.TypeIndex;
            type = typeof(T);
        }

        public List<T> GetAllActiveComponents() => activeComponents;

        public void Remove(int entityId)
        {
            for (int i = 0; i < activeComponents.Count; i++)
            {
                if (activeComponents[i].EntityId == entityId)
                {
                    activeComponents.RemoveAt(i);
                    break;
                }
            }

            ComponentsUpdated?.Invoke(id);
        }

        public T Get(int entityId)
        {
            T component;
            if (pooledComponents.Count > 0)
            {
                component = pooledComponents[0];
            }
            else
            {
                component = (T)Activator.CreateInstance(typeof(T));
            }

            component.Init(entityId);
            activeComponents.Add(component);

            ComponentsUpdated?.Invoke(id);

            return component;
        }

        public void Reset()
        {
            for (int i = 0; i < pooledComponents.Count; i++)
            {
                pooledComponents[i].Reset();
            }

            for (int i = 0; i < activeComponents.Count; i++)
            {
                activeComponents[i].Reset();
            }

            pooledComponents.Clear();
            activeComponents.Clear();

            ComponentsUpdated = null;
        }
    }
}