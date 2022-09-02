using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.Entity;
using CustomEcsBase.World;

namespace CustomEcsBase.Filter.Filters.ComponentsFilter
{
    public class EcsComponentsFilterTwo<T1, T2> : EcsBaseComponentsFilter
        where T1 : IEcsComponent
        where T2 : IEcsComponent
    {
        private List<EcsEntity> entities = new List<EcsEntity>();

        private EcsComponentsPool<T1> pool1;
        private List<T1> components1 = new List<T1>();

        private EcsComponentsPool<T2> pool2;
        private List<T2> components2 = new List<T2>();

        private int componentsCount = 0;

        public List<T1> Get1 => components1;
        public List<T2> Get2 => components2;
        public override int ComponentsCount => componentsCount;


        public override void Init(EcsWorld world)
        {
            base.Init(world);

            InitPool(ref pool1);
            InitPool(ref pool2);

            FillComponents();
        }

        protected override void ProcessAddNewToPool(int poolId, int entityId)
        {
            if (!world.TryGetEntityBy(entityId, out var entity)) return;

            if (entities.Contains(entity)) return;

            var otherPoolId = pool1.Id == poolId ? pool2.Id : pool1.Id;

            if (!entity.HasComponent(otherPoolId)) return;

            entities.Add(entity);
            components1.Add(pool1.Get(entityId));
            components2.Add(pool2.Get(entityId));
            componentsCount++;
        }

        protected override void ProcessRemoveFromPool(int poolId, int entityId)
        {
            EcsEntity currentEntity = null;
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].id == entityId)
                {
                    currentEntity = entities[i];
                    break;
                }
            }

            if (currentEntity == null) return;

            entities.Remove(currentEntity);

            if (!TryRemoveComponent(currentEntity.id, ref components1) ||
                !TryRemoveComponent(currentEntity.id, ref components2))
                return;

            componentsCount--;

            if (componentsCount < 0)
            {
                componentsCount = 0;
            }
        }

        protected sealed override void FillComponents()
        {
            entities.Clear();
            components1.Clear();
            components2.Clear();

            var allEntities = world.Entities;

            for (int i = 0; i < allEntities.Count; i++)
            {
                if (!allEntities[i].HasComponent<T1>() || !allEntities[i].HasComponent<T2>())
                {
                    continue;
                }

                entities.Add(allEntities[i]);
            }


            for (int i = 0; i < entities.Count; i++)
            {
                components1.Add(pool1.Get(entities[i].id));
                components2.Add(pool2.Get(entities[i].id));
            }

            componentsCount = entities.Count;
        }
    }
}