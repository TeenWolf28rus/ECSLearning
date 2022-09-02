using System;
using System.Collections.Generic;
using CustomEcsBase.Components;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.Data;
using CustomEcsBase.Entity;
using CustomEcsBase.Filter.Filters;
using CustomEcsBase.Filter.Manager;

namespace CustomEcsBase.World
{
    public class EcsWorld
    {
        private IEcsEntityManager entityManager;
        private IEcsComponentsManager componentsManager;
        private IEcsFilterManager filterManager;

        private EcsSharedDataContainer sharedDataContainer;
        public EcsSharedDataContainer SharedDataContainer => sharedDataContainer;

        public EcsWorld(EcsSharedDataContainer sharedDataContainer = null)
        {
            this.sharedDataContainer = sharedDataContainer;
            entityManager = new EntitiesManager();
            componentsManager = new EcsComponentsManager();
            filterManager = new EcsFilterManager();
        }

        public EcsEntity GetNewEntity() => entityManager.GetNewEntity(this);

        public List<EcsEntity> Entities => entityManager.Entities;
        public bool TryGetEntityBy(int id, out EcsEntity entity) => entityManager.TryGetEntityBy(id, out entity);

        public void RemoveEntity(EcsEntity entity) => entityManager.RemoveEntity(entity);


        public void Dispose()
        {
            entityManager.Dispose();
            componentsManager.Dispose();
        }

        public EcsComponentsPool<T> GetPool<T>() where T : IEcsComponent => componentsManager.GetPool<T>();

        public bool HasPool<T>() where T : IEcsComponent => componentsManager.HasPool<T>();

        public EcsBaseFilter GetFilter(Type filterType) => filterManager.GetFilter(this, filterType);
    }
}