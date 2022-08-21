using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.Data;
using CustomEcsBase.Entity;

namespace CustomEcsBase.World
{
    public class CEcsWorld
    {
        private List<CEcsEntity> activeEntities = new List<CEcsEntity>();
        private List<CEcsEntity> pooledEntity = new List<CEcsEntity>();

        private List<ICEcsComponentPool> componentPools = new List<ICEcsComponentPool>();
        private CEcsSharedDataContainer sharedDataContainer;

        public List<ICEcsComponentPool> ComponentPools => componentPools;

        public CEcsSharedDataContainer SharedDataContainer => sharedDataContainer;

        public CEcsWorld(CEcsSharedDataContainer sharedDataContainer = null)
        {
            this.sharedDataContainer = sharedDataContainer;
        }

        public CEcsEntity GetNewEntity()
        {
            CEcsEntity entity;

            if (pooledEntity.Count > 0)
            {
                entity = pooledEntity[0];
            }
            else
            {
                entity = new CEcsEntity(this, 0);
            }

            entity.id = activeEntities.Count;
            activeEntities.Add(entity);

            return entity;
        }

        public void ReturnToPool(CEcsEntity entity)
        {
            activeEntities.Remove(entity);
            pooledEntity.Add(entity);
        }


        public void Dispose()
        {
            for (int i = 0; i < componentPools.Count; i++)
            {
                componentPools[i].Reset();
            }

            componentPools.Clear();

            activeEntities.Clear();
            pooledEntity.Clear();
        }

        public CEcsComponentsPool<T> GetPool<T>() where T : ICEcsComponent
        {
            var typeIndex = CEcsComponentPoolIndex<T>.TypeIndex;
            if (typeIndex >= componentPools.Count)
            {
                componentPools.Add(new CEcsComponentsPool<T>());
            }

            return (CEcsComponentsPool<T>)componentPools[typeIndex];
        }
    }
}