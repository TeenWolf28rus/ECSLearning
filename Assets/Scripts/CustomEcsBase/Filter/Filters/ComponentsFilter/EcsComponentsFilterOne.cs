using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.World;

namespace CustomEcsBase.Filter.Filters.ComponentsFilter
{
    public class EcsComponentsFilterOne<T1> : EcsBaseComponentsFilter where T1 : IEcsComponent
    {
        private EcsComponentsPool<T1> pool1;
        private List<T1> components1 = new List<T1>();
        private int componentsCount = 0;

        public List<T1> Get1 => components1;
        public override int ComponentsCount => componentsCount;

        public override void Init(EcsWorld world)
        {
            base.Init(world);
            
            InitPool(ref pool1);
            FillComponents();
        }

        protected override void ProcessAddNewToPool(int poolId, int entityId)
        {
            FillComponents();
        }

        protected override void ProcessRemoveFromPool(int poolId, int entityId)
        {
            if (!TryRemoveComponent(entityId, ref components1)) return;

            componentsCount--;
            if (componentsCount < 0)
            {
                componentsCount = 0;
            }
        }

        protected sealed override void FillComponents()
        {
            components1 = pool1.GetAllActiveComponents();
            componentsCount = components1.Count;
        }
    }
}